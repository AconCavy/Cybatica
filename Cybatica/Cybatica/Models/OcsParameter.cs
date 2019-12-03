using System.Text.Json.Serialization;

namespace Cybatica.Models
{
    public class OcsParameter
    {
        [JsonPropertyName("Parameter")] public string Name { get; set; }
        public float NnMean { get; set; }
        public float SdNn { get; set; }
        public float MeanEda { get; set; }
        public float PeakEda { get; set; }
        public float Intercept { get; set; }
    }
}