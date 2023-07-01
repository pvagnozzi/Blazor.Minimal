namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public interface IRegistrableModule
{
    void Register(IEndpointRouteBuilder endpointRouteBuilder);
}

