using Newtonsoft.Json;

namespace MatFrem.Models.ViewModel
{
    public class GeoJson
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public class Properties
    {
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}


