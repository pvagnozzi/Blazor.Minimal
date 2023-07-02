using Microsoft.AspNetCore.Mvc;

namespace Blazor.Minimal.Modules.Payloads;

public record PagedQueryFilters : QueryFilters
{
    [FromQuery(Name = "pageIndex")]
    public int? PageIndex { get; set; } = 0;

    [FromQuery(Name = "pageSize")]
    public int? PageSize { get; set; } = 10;
}
