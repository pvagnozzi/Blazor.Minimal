using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Blazor.Minimal.Modules;

public static class ConfigurationExtensions
{
    internal static ModuleManager ModuleManager { get; } = new();

    public static IServiceCollection AddModules(this IServiceCollection services,
        IEnumerable<Assembly>? assemblies = null, string[]? origins = null, bool isDevelopment = false)
    {
        assemblies ??= new[] { Assembly.GetCallingAssembly() };
        ModuleManager.AddModules(assemblies);
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
                    var or = origins?.ToArray() ?? Array.Empty<string>();
                    if (isDevelopment && !or.Any())
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins(or);
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

    public static RouteHandlerBuilder SetOpenApi(this RouteHandlerBuilder builder, string[] tags, string? name = null,
        string? displayName = null, string? description = null, string? summary = null, string? securityPolicy = null,
        params object[] metadata)
    {
        var result = builder.WithOpenApi()
            .RequireCors("publicapi")
            .WithTags(tags);

        if (securityPolicy is not null)
        {
            builder.RequireAuthorization(securityPolicy);
            if (securityPolicy == string.Empty)
            {
                builder.RequireAuthorization();
            }
        }

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

    public static RouteHandlerBuilder SetOpenApi(this RouteHandlerBuilder builder, string groupName,
        string? name = null,
        string? displayName = null, string? description = null, string? summary = null, string? securityPolicy = null,
        params object[] metadata) => builder.SetOpenApi(new[] { groupName }, name, displayName, description, summary,
        securityPolicy, metadata);

}

