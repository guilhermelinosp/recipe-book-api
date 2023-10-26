using Microsoft.EntityFrameworkCore;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Infrastructure.Persistence;

public class InfrastructureDbContext : DbContext
{
    public InfrastructureDbContext(DbContextOptions<InfrastructureDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureDbContext).Assembly);
    }

    public DbSet<User>? Users { get; set; }
}

