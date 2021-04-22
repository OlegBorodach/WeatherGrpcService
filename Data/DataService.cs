using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        public DataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private Uri BuildUri(string cityName, string stateCode)
        {
            var builder = new UriBuilder("http", "api.openweathermap.org")
            {
                Path = "data/2.5/weather",
                Query = $"APPID=923b1b2de969855de132ccc64b710c12&units=metric&lang=ru&q={cityName},{stateCode}"
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
