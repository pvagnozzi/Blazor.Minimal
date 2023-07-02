using System.Text.Json.Serialization;
using Blazor.Minimal.Modules.Payloads;

namespace Blazor.Minimal.Example.Server.Features.Weather.Payloads;

public record WeatherQueryFilters : QueryFilters
{
    [JsonPropertyName("days")]
    [JsonRequired]
    public int Days { get; set; }
}

