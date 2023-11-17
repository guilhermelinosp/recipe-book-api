using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Contexts;
using RecipeBook.Infrastructure.Repositories;

namespace RecipeBook.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .AddDatabase()
            .AddRepositories();
        
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySql(
                Environment.GetEnvironmentVariable("SQL-ConnectionString")!,
                new MySqlServerVersion(new Version(8, 2, 0)),
                options => options.EnableRetryOnFailure());
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepositoryImp>();
        services.AddScoped<IRecipeRepository, RecipeRepositoryImp>();

        return services;
    }
}