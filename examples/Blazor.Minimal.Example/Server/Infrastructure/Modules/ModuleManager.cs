using System.Reflection;

namespace Blazor.Minimal.Example.Server.Infrastructure.Modules;

public class ModuleManager
{
    public IList<Type> Modules { get; } = new List<Type>();

    public void AddModule(Type type)
    {
        if (!type.IsAssignableTo(typeof(IRegistrableModule)))
        {
            throw new InvalidDataException($"{type.FullName} is not a module");
        }

        if (Modules.Contains(type))
        {
            return;
        }

        Modules.Add(type);
    }

    public void AddModulesFromAssembly(Assembly assembly)
    {
        var moduleTypes = assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IRegistrableModule)));

        foreach (var moduleType in moduleTypes)
        {
            AddModule(moduleType);
        }
    }

    public void RegisterModules(IServiceCollection serviceCollection)
    {
        foreach (var module in Modules)
        {
            serviceCollection.AddScoped(module);
        }
    }

    public WebApplication MapEndPoints(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        foreach (var moduleType in Modules)
        {
            var module = (IRegistrableModule)scope.ServiceProvider.GetRequiredService(moduleType);
            module.Register(app);
        }

        return app;
    }
}

