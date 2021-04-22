using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using WeatherGrpcService.Data;
using WeatherGrpcService.Model;

namespace WeatherGrpcService.Services
{
    public class CurrentWeatherService : CurrentWeather. CurrentWeatherBase
    {
        private readonly ILogger<Weather> _logger;
        private readonly IDataService _dataService;
        private readonly IMapper _mapper;

        public CurrentWeatherService(ILogger<Weather> logger, IDataService dataService, IMapper mapper)
        {
            _logger = logger;
            _dataService = dataService;
            _mapper = mapper;
        }

        public override async Task<WeatherResponse> GetWeather(WeatherRequest request, ServerCallContext context)
        {
            var mainWeather= await _dataService.GetCurrentWeather(request.CityName, request.StateCode);
            return _mapper.Map<MainWeather, WeatherResponse>(mainWeather);
        }
    }
}
