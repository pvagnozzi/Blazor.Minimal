namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public interface IModuleResponse : IModuleMessage
{
    bool IsSuccess { get; }

    string? Message { get; }
}