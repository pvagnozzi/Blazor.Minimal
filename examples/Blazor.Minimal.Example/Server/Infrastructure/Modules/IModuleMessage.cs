namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public interface IModuleMessage
{
    string? CorrelationId { get; }
}

