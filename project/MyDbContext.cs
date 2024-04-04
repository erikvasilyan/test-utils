using Microsoft.EntityFrameworkCore;

namespace project;

public sealed class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }
}