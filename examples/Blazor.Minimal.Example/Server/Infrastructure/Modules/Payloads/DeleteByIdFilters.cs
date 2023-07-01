using Microsoft.AspNetCore.Mvc;

namespace Blazor.Minimal.Example.Server.Infrastructure.Modules.Payloads;

public record DeleteByIdFilter : QueryFilters
{
    [FromRoute(Name = "Id")]
    public string Id { get; set; } = string.Empty;
}
