using System.Text.Json.Serialization;

namespace Blazor.Minimal.Example.Shared.Modules.Payloads;

public record WeatherForecast
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("summary")]
    public WeatherSummary Summary { get; set; }

    [JsonPropertyName("temperatureF")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
