namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public class ModuleException : Exception
{
    public ModuleException(string message, string? correlationId = null) : base(message)
    {
        CorrelationId = correlationId;
    }

    public ModuleException(string message, Exception innerException, string? correlationId = null) : base(message,
        innerException)
    {
        CorrelationId = correlationId;
    }

    public string? CorrelationId { get; init; }
}