using Newtonsoft.Json;

namespace WeatherGrpcService.Model{ 

    public class Sys
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("sunrise")]
        public int Sunrise { get; set; }

        [JsonProperty("sunset")]
        public int Sunset { get; set; }
    }

}