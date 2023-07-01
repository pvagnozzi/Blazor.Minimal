using System.Text.Json.Serialization;

namespace Blazor.Minimal.Example.Server.Infrastructure.Modules.Payloads;

public record ModuleResponse : IModuleResponse
{
    public ModuleResponse(string? correlationId = null, bool isSuccess = true, string? message = null)
    {
        CorrelationId = correlationId ?? Guid.NewGuid().ToString();
        IsSuccess = isSuccess;
        Message = message;
    }

    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }
}

public record ModuleResponse<TContent> : ModuleResponse
{
    public ModuleResponse(TContent content, string? correlationId = null, bool isSuccess = true,
        string? message = null) :
        base(correlationId, isSuccess, message)
    {
        Content = content;
    }

    [JsonPropertyName("content")] 
    public TContent Content { get; init; }
}