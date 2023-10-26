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
        var serverConnection = configuration["ConnectionString"];
        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(serverConnection));

        services.AddDbContext<InfrastructureDbContext>(options =>
        {
            options
                .UseMySql(serverConnection, serverVersion)
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