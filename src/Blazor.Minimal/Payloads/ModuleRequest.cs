using System.Text.Json.Serialization;

namespace Blazor.Minimal.Modules.Payloads;

public record ModuleRequest : IModuleRequest
{
    public ModuleRequest(string? correlationId = null)
    {
        CorrelationId = correlationId ?? Guid.NewGuid().ToString();
    }

    [JsonPropertyName("correlationId")]
    public string? CorrelationId { get; init; }
}


public record ModuleRequest<TContent> : ModuleRequest where TContent : class, new()
{
    public ModuleRequest(TContent? content = null, string? correlationId = null) : base(correlationId)
    {
        Content = content ?? new TContent();
    }

    [JsonPropertyName("content")]
    public TContent Content { get; init; }
}