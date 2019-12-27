using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarsImages.Internal.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MarsImages.Internal.Services
{
    public class MarsImageMetaDataService : IImageMetaDataService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MarsImageMetaDataService(ILogger<MarsImageMetaDataService> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<Photo>> GetMetaDataByDateAsync(DateTime date, int page = 1, string roverName = "curiosity")
        {
            var results = new List<Photo>();
            try
            {
                var queryString = await GenerateQueryStringAsync(date, page, roverName);
                var baseUrl = $"{_httpClient.BaseAddress}/{roverName}/photos?{queryString}";
                var uri = new Uri(baseUrl);
                var response = await _httpClient.GetStringAsync(uri);
                var photos = JsonConvert.DeserializeObject<ImageResponse>(response);
                results = photos.Photos;
            }
            catch (Exception e)
            {
                _logger?.LogError(e, e.Message);
            }
            return results;
        }

        private async Task<string> GenerateQueryStringAsync(DateTime date, int page, string roverName)
        {
            //  https://api.nasa.gov/mars-photos/api/v1/rovers/curiosity/photos?earth_date=2015-6-3&api_key=DEMO_KEY
            //  base: https://api.nasa.gov/mars-photos/api/v1/rovers
            //  earth_date
            //  camera
            //  page
            //  api_key
            var result = string.Empty;
            var values = new KeyValuePair<string, string>[] {
                new KeyValuePair<string, string>("earth_date", date.ToString("yyyy-M-d")),
                new KeyValuePair<string, string>("page", page.ToString()),
                new KeyValuePair<string, string>("api_key",  _configuration["NASA_Api_Key"])
            };
            using (var content = new FormUrlEncodedContent(values))
            {
                result = await content.ReadAsStringAsync();
            }
            return result;
        }
    }
}