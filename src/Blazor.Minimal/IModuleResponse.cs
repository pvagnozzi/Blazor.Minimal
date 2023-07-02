namespace Blazor.Minimal.Modules;

public interface IModuleResponse : IModuleMessage
{
    bool IsSuccess { get; }

    string? Message { get; }
}