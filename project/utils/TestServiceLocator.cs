using Microsoft.Extensions.DependencyInjection;

namespace project.utils;

public class TestServiceNotFoundException(Type type) : Exception($"Service {type} not found in test dependencies.");

public class TestServiceLocator
{
    public TestServiceLocator()
    {
        var fixture = new TestDatabaseFixture();
        var services = new ServiceCollection();
        ApplicationDependencies.AddDependencies(services);
        services.AddScoped<MyDbContext>(_ => fixture.Context);

        Provider = services.BuildServiceProvider();
    }

    private ServiceProvider Provider { get; }

    public TService Get<TService>()
    {
        return Provider.GetService<TService>()
               ?? throw new TestServiceNotFoundException(typeof(TService));
    }
}