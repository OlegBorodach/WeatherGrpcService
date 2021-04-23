using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WeatherGrpcService.Model;

namespace WeatherGrpcService.Data
{
    public interface IDataService
    {
        Task<MainWeather> GetCurrentWeather(string cityName, string stateCode);
    }

    public class DataService : IDataService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public DataService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        private Uri BuildUri(string cityName, string stateCode)
        {
            var host = _configuration.GetSection("WeatherSettings:Host").Value;
            var units = _configuration.GetSection("WeatherSettings:Units").Value;
            var lang = _configuration.GetSection("WeatherSettings:Lang").Value;
            var appId = _configuration.GetSection("WeatherSettings:APPID").Value;

            var builder = new UriBuilder("http", host)
            {
                Query = $"APPID={appId}&units={units}&lang={lang}&q={cityName},{stateCode}"
            };
            return builder.Uri;
        }
        
        public async Task<MainWeather> GetCurrentWeather(string cityName, string stateCode)
        {
            var uri = BuildUri(cityName, stateCode);
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(uri);

            if (response.StatusCode != HttpStatusCode.OK)
                throw new InvalidOperationException();

            var weatherStr = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<MainWeather>(weatherStr);
        }
    }
}
