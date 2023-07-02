namespace Blazor.Minimal.Modules.Payloads;

public record ModuleListResponse<TItem>
    (IEnumerable<TItem> Items, string? CorrelationId = null) : ModuleResponse<TItem[]>(Items.ToArray(), CorrelationId);