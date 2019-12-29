using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace MarsImages.Internal.Services
{
    public class MarsImageHttpClient : IMarsImageHttpClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        public MarsImageHttpClient(ILogger<MarsImageHttpClient> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }
        public async Task<Stream> CacheMarsImageAsync(Data.Models.Photo photoMetaData)
        {
            if (photoMetaData == null) return null;
            if (string.IsNullOrEmpty(photoMetaData.Source)) return null;
            var uri = new Uri(photoMetaData.Source);
            try
            {
                var stream = await _httpClient.GetStreamAsync(uri);
                return stream;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, e.Message);
            }
            return null;
        }
    }
}