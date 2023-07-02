using Blazor.Minimal.Example.Server.Features.Weather.Payloads;
using Blazor.Minimal.Example.Shared.Modules.Payloads;
using Blazor.Minimal.Modules;
using Blazor.Minimal.Modules.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Minimal.Example.Server.Features.Weather;

public class WeatherForecastModule : RegistrableModule
{
    public override void Register(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/forecast", GetWeatherDefault)
            .SetOpenApi("Weather");
        endpointRouteBuilder.MapGet("/forecast/filtered/", GetWeatherFiltered)
            .SetOpenApi("Weather");
        endpointRouteBuilder.MapPost("/forecast/", SetWeather)
            .SetOpenApi("Weather");
        endpointRouteBuilder.MapDelete("/forecast/", DeleteWeather)
            .SetOpenApi("Weather");
    }

    public static WeatherForecast[] GetWeatherDefault() => GetWeather(5);


    public static ModuleListResponse<WeatherForecast> GetWeatherFiltered(HttpContext context, ILogger<RegistrableModule> logger,
        [AsParameters] WeatherQueryFilters filters)
    {
        var user = context.User.Identity?.Name ?? "Anonymous";
        logger.LogInformation("GetWeather called with {Days} days from {user}", filters.Days, user);
        var data = GetWeather(filters.Days);
        return new ModuleListResponse<WeatherForecast>(data, filters.CorrelationId);
    }

    public static ModuleResponse SetWeather(HttpContext context, ILogger<RegistrableModule> logger,
        [FromBody] ModuleRequest<WeatherForsecastInput> request)
    {
        var user = context.User.Identity?.Name ?? "Anonymous";
        logger.LogInformation("SetWeather called from {user}: {content}", user, request.Content);

        return new ModuleResponse(request.CorrelationId);
    }

    public static ModuleResponse DeleteWeather(HttpContext context, ILogger<RegistrableModule> logger,
        [FromBody] DeleteByIdFilter filters)
    {
        var user = context.User.Identity?.Name ?? "Anonymous";
        logger.LogInformation("SetWeather called from {user}: {content}", user, filters);

        return new ModuleResponse(filters.CorrelationId);
    }

    private static WeatherSummary GetRandomWeatherSummary()
    {
        var idx = Random.Shared.Next(0, Enum.GetValues<WeatherSummary>().Length - 1);
        return (WeatherSummary)idx;
    }

    private static WeatherForecast[] GetWeather(int days) =>
        Enumerable.Range(1, days).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = GetRandomWeatherSummary()
        })
        .ToArray();
}
