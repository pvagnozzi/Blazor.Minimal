using Microsoft.AspNetCore.Routing;

namespace Blazor.Minimal.Modules;

public interface IRegistrableModule
{
    void Register(IEndpointRouteBuilder endpointRouteBuilder);
}

