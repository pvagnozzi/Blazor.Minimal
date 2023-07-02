using System.Text.Json.Serialization;

namespace Blazor.Minimal.Modules.Payloads;

public record ModulePagedListResponse<TItem> : ModuleListResponse<TItem>
{
    public ModulePagedListResponse() : this(Array.Empty<TItem>())
    {
    }

    public ModulePagedListResponse(IEnumerable<TItem> items, long? totalCount = null, int? totalPages = null,
        int? pageIndex = null, int? pageSize = null, string? correlationId = null) : base(items.ToArray(),
        correlationId)
    {
        PageIndex = pageIndex ?? 0;
        PageSize = pageSize ?? Content.Length;
        TotalCount = totalCount ?? PageSize;
        TotalPages = totalPages ?? 1;
    }

    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; init; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }

    [JsonPropertyName("totalCount")]
    public long TotalCount { get; init; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; init; }
}