using Newtonsoft.Json;

namespace WetherGrpcService.Model
{ 
    public class Clouds
    {
        [JsonProperty("all")]
        public int All { get; set; }
    }

}