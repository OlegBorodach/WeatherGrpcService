using System.Collections.Generic;
using AutoMapper;
using Google.Protobuf.Collections;
using WeatherGrpcService.Model;

namespace WeatherGrpcService.Mapping
{
    public class WeatherMapping:Profile
    {
        public WeatherMapping()
        {
            CreateMap<MainWeather, WeatherResponse>();
            CreateMap<Model.Weather, Weather>();
            CreateMap<Model.Coord, Coord>();
            CreateMap<Model.Main, Main>();
            CreateMap<Model.Sys, Sys>();
            CreateMap<Model.Wind, Wind>();
            CreateMap<List<Model.Weather>, RepeatedField<Weather>>().ConvertUsing(MappingFunction);
        }

        private RepeatedField<Weather> MappingFunction(IEnumerable<Model.Weather> source, RepeatedField<Weather> destination, ResolutionContext context)
        {
            destination ??= new RepeatedField<Weather>();
            foreach (var item in source)
            {
                destination.Add(context.Mapper.Map<Weather>(item));
            }
            return destination;
        }
    }
}
