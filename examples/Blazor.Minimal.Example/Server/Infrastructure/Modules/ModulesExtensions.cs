using System.Diagnostics;
using Microsoft.OpenApi.Models;

namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public static class ConfigurationExtensions
{
    internal static ModuleManager ModuleManager { get; } = new();

    public static IServiceCollection AddModules(this IServiceCollection services, string[]? origins = null, bool isDevelopment = false)
    {
        ModuleManager.AddModulesFromAssembly(typeof(ConfigurationExtensions).Assembly);
        ModuleManager.RegisterModules(services);

        return services
            .AddAuthorization()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blazor Minimal", Version = "v1" }))
            .AddCors(options =>
            {
                options.AddPolicy("publicapi", o =>
                {
                    var builder = o.AllowAnyHeader().AllowAnyMethod();
                    if (isDevelopment)
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        if (!(origins?.Any() ?? false))
                        {
                            throw new InvalidOperationException("No valid listening urls configured");
                        }

                        builder.WithOrigins(origins);
                    }
                });
            });
    }

    public static WebApplication UseModules(this WebApplication application)
    {
        application
            .UseSwagger()
            .UseSwaggerUI()
            .UseCors()
            .UseHttpsRedirection()
            .UseExceptionHandler(app => app.Run(ExceptionHandler.HandleExceptionAsync));
        return ModuleManager.MapEndPoints(application);
    }

    [DebuggerStepThrough]
    public static RouteHandlerBuilder SetOpenApi(this RouteHandlerBuilder builder, string[] tags, string? name = null,
        string? displayName = null, string? description = null, string? summary = null, string? securityPolicy = null,
        params object[] metadata)
    {
        var result = builder.WithOpenApi()
            .RequireCors("publicapi")
            .WithTags(tags);

        if (name is not null)
        {
            result = result.WithSummary(name);
        }

        if (displayName is not null)
        {
            result = result.WithName(displayName);
        }

        if (description is not null)
        {
            result = result.WithDescription(description);
        }

        if (summary is not null)
        {
            result = result.WithSummary(summary);
        }

        if (metadata.Any())
        {
            result = result.WithMetadata(metadata);
        }

        return result;
    }

    [DebuggerStepThrough]
    public static RouteHandlerBuilder SetOpenApi(this RouteHandlerBuilder builder, string groupName,
        string? name = null,
        string? displayName = null, string? description = null, string? summary = null, string? securityPolicy = null,
        params object[] metadata) => builder.SetOpenApi(new[] { groupName }, name, displayName, description, summary,
        securityPolicy, metadata);
}

