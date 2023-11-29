using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Infrastructure.Contexts;

public class RecipeBookDbContext : DbContext
{
    public RecipeBookDbContext(DbContextOptions<RecipeBookDbContext> options) : base(options)
    {
    }

    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Recipe>? Recipes { get; set; }
    public DbSet<Code>? Codes { get; set; }
    public DbSet<Connection>? Connections { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}