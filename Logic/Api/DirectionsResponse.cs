using Newtonsoft.Json;

public class DirectionsResponse
{
    [JsonProperty("status")]
    public required string Status { get; set; }

    [JsonProperty("error_message")]
    public string? ErrorMessage { get; set; }

    [JsonProperty("routes")]
    public List<Route>? Routes { get; set; }

    public class Route
    {
        public required List<Leg> Legs { get; set; }
    }

    public class Leg
    {
        public required string StartAddress { get; set; }
        public required string EndAddress { get; set; }
    }
}

