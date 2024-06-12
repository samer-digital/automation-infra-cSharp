using Newtonsoft.Json;

public class ContextOptions
{
    [JsonProperty("viewport")]
    public ViewportOptions? Viewport { get; set; }

    [JsonProperty("userAgent")]
    public string? UserAgent { get; set; }

    [JsonProperty("locale")]
    public string? Locale { get; set; }

    [JsonProperty("timezoneId")]
    public string? TimezoneId { get; set; }

    [JsonProperty("geolocation")]
    public GeolocationOptions? Geolocation { get; set; }

    [JsonProperty("permissions")]
    public List<string>? Permissions { get; set; }

    [JsonProperty("ignoreHTTPSErrors")]
    public bool IgnoreHTTPSErrors { get; set; }

    [JsonProperty("javaScriptEnabled")]
    public bool JavaScriptEnabled { get; set; }

    [JsonProperty("deviceScaleFactor")]
    public int DeviceScaleFactor { get; set; }

    [JsonProperty("isMobile")]
    public bool IsMobile { get; set; }

    [JsonProperty("hasTouch")]
    public bool HasTouch { get; set; }

    [JsonProperty("defaultBrowserType")]
    public string? DefaultBrowserType { get; set; }
}

public class ViewportOptions
{
    [JsonProperty("width")]
    public int Width { get; set; }

    [JsonProperty("height")]
    public int Height { get; set; }
}

public class GeolocationOptions
{
    [JsonProperty("latitude")]
    public float Latitude { get; set; }

    [JsonProperty("longitude")]
    public float Longitude { get; set; }
}