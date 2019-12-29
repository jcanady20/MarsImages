using System;
using System.Net.Http;
using System.Threading.Tasks;
using MarsImages.Internal.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MarsImages.Internal.Services
{
    public class MarsImageMetaDataHttpClient : IMarsImageMetaDataHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public MarsImageMetaDataHttpClient(ILogger<MarsImageMetaDataHttpClient> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public Uri BaseAddress => _httpClient?.BaseAddress;

        public async Task<ImageResponse> GetRoverMetaDataByDateAsync(Uri uri)
        {
            var response = await _httpClient.GetStringAsync(uri);
            var result = JsonConvert.DeserializeObject<ImageResponse>(response);
            return result;
        }
    }
}