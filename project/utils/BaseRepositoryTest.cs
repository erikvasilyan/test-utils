namespace project.utils;

public abstract class BaseRepositoryTest<TRepository> : IDisposable, IAsyncDisposable
    where TRepository : class
{
    protected BaseRepositoryTest()
    {
        var serviceLocator = new TestServiceLocator();
        Context = serviceLocator.Get<MyDbContext>();
        Sut = serviceLocator.Get<TRepository>();
    }

    protected TRepository Sut { get; }
    protected MyDbContext Context { get; }

    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}