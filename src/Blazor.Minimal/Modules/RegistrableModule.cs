using Microsoft.AspNetCore.Routing;

namespace Blazor.Minimal.Modules;

public class RegistrableModule : IRegistrableModule
{
    public virtual void Register(IEndpointRouteBuilder endpointRouteBuilder)
    {
    }
}

