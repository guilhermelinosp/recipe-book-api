using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Repositories;
using RecipeBook.Infrastructure.Persistence;
using RecipeBook.Infrastructure.Persistence.Repositories;

namespace RecipeBook.Infrastructure;

public static class InfrastructureModule
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
        services.AddDbContext<DataContext>(opt =>
            opt.UseMySql(configuration["ConnectionString"], new MySqlServerVersion(new Version(8, 0, 27))));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepositoryImp>();

        return services;
    }
}