using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Persistence;
using RecipeBook.Infrastructure.Persistence.Repositories;

namespace RecipeBook.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMySql(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration configuration)
    {
        bool.TryParse(configuration["InMemory"], out var inmemory);

        if (inmemory)
        {
            services.AddDbContext<InfrastructureDbContext>(options =>
            {
                options
                    .UseInMemoryDatabase("InMemoryDbForTesting")
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            });

            return services;
        }

        var serverConnection = configuration["ConnectionString"];

        services.AddDbContext<InfrastructureDbContext>(options =>
        {
            options
                .UseMySql(serverConnection, new MySqlServerVersion(new Version(8, 0, 28))) // Update the version as needed
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });

        return services;
    }


    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepositoryImp>();

        return services;
    }


}