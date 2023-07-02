using System.Net;
using System.Text.Json;
using Blazor.Minimal.Modules.Payloads;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Blazor.Minimal.Modules;

public class ExceptionHandler
{
    public static Task HandleExceptionAsync(HttpContext context)
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error ?? new InvalidOperationException("Unexpected exception");
        var statusCode = GetStatusCode(exception.GetType());
        var correlationId = exception is ModuleException moduleException
            ? moduleException.CorrelationId
            : Guid.NewGuid().ToString();

        var response = new ModuleResponse(correlationId, false, exception.Message);
        var exceptionResult = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(exceptionResult);
    }

    private static HttpStatusCode GetStatusCode(Type exceptionType) => exceptionType switch
    {
        _ when exceptionType == typeof(NotImplementedException) => HttpStatusCode.NotImplemented,
        _ when exceptionType == typeof(ArgumentException) => HttpStatusCode.BadRequest,
        _ when exceptionType == typeof(ArgumentNullException) => HttpStatusCode.BadRequest,
        _ when exceptionType == typeof(ArgumentOutOfRangeException) => HttpStatusCode.BadRequest,
        _ when exceptionType == typeof(InvalidOperationException) => HttpStatusCode.BadRequest,
        _ => HttpStatusCode.InternalServerError
    };
}