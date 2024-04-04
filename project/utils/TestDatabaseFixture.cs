using Microsoft.EntityFrameworkCore;

namespace project.utils;

public class TestDatabaseFixture
{
    private static readonly object Lock = new();
    private static bool _databaseInitialized;

    public TestDatabaseFixture()
    {
        Context = CreateContext();
        lock (Lock)
        {
            if (_databaseInitialized) return;
            Context.Database.EnsureDeleted();
            Context.Database.Migrate();
            _databaseInitialized = true;
        }
    }

    public MyDbContext Context { get; }

    private MyDbContext CreateContext()
    {
        return new MyDbContext(
            new DbContextOptionsBuilder<MyDbContext>()
                .UseSqlServer("Data Source=sqlserver,1433;Database=myDatabase;User ID=myUser;Password=myPassword;trusted_connection=false;Persist Security Info=False;Encrypt=False")
                .Options
        );
    }
}