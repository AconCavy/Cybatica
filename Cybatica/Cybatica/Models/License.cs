using System.Text.Json.Serialization;

namespace Cybatica.Models
{
    public class License
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("android")] public bool IsVisibleAndroid { get; set; }

        [JsonPropertyName("ios")] public bool IsVisibleiOS { get; set; }

        [JsonPropertyName("license_type")] public string LicenseType { get; set; }

        [JsonPropertyName("url")] public string Url { get; set; }

        [JsonPropertyName("copyright")] public string Copyright { get; set; }
    }
}