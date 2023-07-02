using System.Text.Json.Serialization;
using Blazor.Minimal.Modules.Payloads;

namespace Blazor.Minimal.Example.Server.Features.Weather.Payloads;

public record WeatherDeleteQueryFilters : QueryFilters
{
    [JsonPropertyName("date")]
    [JsonRequired]
    public DateOnly Date { get; set; }

    [JsonPropertyName("days")]
    [JsonRequired]
    public int Days { get; set; }
}

