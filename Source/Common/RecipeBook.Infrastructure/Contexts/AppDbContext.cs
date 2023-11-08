using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Recipe>? Recipes { get; set; }
    public DbSet<Ingredient>? Ingredients { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}