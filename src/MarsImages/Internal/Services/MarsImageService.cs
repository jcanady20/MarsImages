using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MarsImages.Internal.Services
{
    public class MarsImageService : IImageService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public MarsImageService(ILogger<MarsImageService> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<string> CacheMarsImageAsync(Data.Models.Photo photoMetaData)
        {
            if (photoMetaData == null) return null;
            if (string.IsNullOrEmpty(photoMetaData.Source)) return null;
            var uri = new Uri(photoMetaData.Source);
            try
            {
                var stream = await _httpClient.GetStreamAsync(uri);
                var localPath = BuildLocalFilePath(photoMetaData);
                await WriteStreamToLocalStorageAsync(localPath, stream);
                return localPath;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
            }
            return null;
        }

        private string BuildLocalFilePath(Data.Models.Photo photoMetaData)
        {
            var basePath = _configuration["ImageStorage"];
            var path =  Path.Combine(basePath, photoMetaData.Rover.Name, photoMetaData.Date.ToString("yyyy-MM-dd"));
            EnsureDirectory(path);
            var result = Path.Combine(path, photoMetaData.FileName);
            return result;
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

        private void EnsureDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }
    }
}