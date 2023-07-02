using System.Text.Json.Serialization;

namespace Blazor.Minimal.Example.Shared.Modules.Payloads;

public record WeatherForsecastInput 
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("summary")]
    public WeatherSummary Summary { get; set; }
}

