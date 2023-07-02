using Microsoft.AspNetCore.Mvc;

namespace Blazor.Minimal.Modules.Payloads;

public record DeleteByIdFilter : QueryFilters
{
    [FromRoute(Name = "Id")]
    public string Id { get; set; } = string.Empty;
}
