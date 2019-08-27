using System;
using Newtonsoft.Json;

namespace Cybatica.Models
{
    [JsonObject("license")]
    public class License
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("android")] public bool IsVisibleAndroid { get; set; }

        [JsonProperty("ios")] public bool IsVisibleiOS { get; set; }

        [JsonProperty("license_type")] public string LicenseType { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("copyright")] public string Copyright { get; set; }
    }
}