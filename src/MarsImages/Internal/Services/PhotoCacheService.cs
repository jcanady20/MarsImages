using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MarsImages.Internal.Data;
using MarsImages.Internal.Extensions;
using System.Linq;

namespace MarsImages.Internal.Services
{
    public class PhotoCacheService : IPhotoCacheService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly IImageMetaDataService _imageMetaDataService;
        private readonly IMarsImageHttpClient _imageService;
        public PhotoCacheService(ILogger<PhotoCacheService> logger, IConfiguration configuration, IMemoryCache memoryCache, IImageMetaDataService imageMetaDataService, IMarsImageHttpClient imageService)
        {
            _logger = logger;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _imageMetaDataService = imageMetaDataService;
            _imageService = imageService;
        }

        public void InitializeCache()
        {
            var dateFile = _configuration["Pre_Cache_File"];
            if (!File.Exists(dateFile)) throw new FileNotFoundException("Unable to locate specified Pre_Cache_File");
            using (var fileStream = File.OpenRead(dateFile))
            {
                var reader = new StreamReader(fileStream);
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    var dt = line.ToDateTime();
                    var idt = new Data.Models.ImageDate();
                    idt.Name = line;
                    idt.Status = (dt == null) ? "Invalid Date" : "Pending";
                    idt.Date = (dt == null) ? System.DateTime.MinValue : dt.Value;
                    _memoryCache.Add(idt);
                }
            }
        }

        public async Task PreFetchImageMetaDataAsync()
        {
            var tasks = _memoryCache.Dates.Where(x => x.Status == "Pending").Select(async idt => {
                _logger.LogInformation($"Started Pre Fetching meta data for {idt.Date}");
                idt.Photos = await _imageMetaDataService.GetMetaDataByDateAsync(idt.Date);
                _logger.LogInformation($"Completed Pre Fetching meta data for {idt.Date}");
            });
            await Task.WhenAll(tasks);
        }

        public async Task CacheImagesAsync()
        {
            foreach (var idt in _memoryCache.Dates.Where(x => x.Status == "Pending"))
            {
                await CacheImagesAsync(idt);
            }
        }

        private async Task CacheImagesAsync(Data.Models.ImageDate imageDate)
        {
            var localStorage = _configuration["ImageStorage"];
            EnsureDirectory(localStorage);
            imageDate.Status = "Caching Images";
            var tasks = imageDate.Photos.Select(async photo => {
                _logger.LogInformation($"Started Pre Fetching image data for {photo.Id}");
                using(var stream = await _imageService.CacheMarsImageAsync(photo))
                {
                    //  If the returned stream is null, something bad happened, abort now
                    if (stream != null)
                    {
                        photo.LocalSource = BuildLocalFilePath(photo);
                        await WriteStreamToLocalStorageAsync(photo.LocalSource, stream);
                    }
                }
                _logger.LogInformation($"Completed Pre Fetching image data for {photo.Id}");
            });
            await Task.WhenAll(tasks);
            imageDate.Status = "Completed";
        }

        private async Task WriteStreamToLocalStorageAsync(string filePath, Stream stream)
        {
            if (File.Exists(filePath)) return;
            using (var fileStream = File.Create(filePath))
            {
                //stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream);
            }
        }

        private string BuildLocalFilePath(Data.Models.Photo photoMetaData)
        {
            var basePath = _configuration["ImageStorage"];
            var path =  Path.Combine(basePath, photoMetaData.Rover.Name, photoMetaData.Date.ToString("yyyy-MM-dd"));
            EnsureDirectory(path);
            var result = Path.Combine(path, photoMetaData.FileName);
            return result;
        }

        private void EnsureDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }
    }
}