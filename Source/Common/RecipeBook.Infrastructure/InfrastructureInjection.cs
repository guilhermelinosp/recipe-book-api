using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Domain.Repositories;
using RecipeBook.Domain.SendGrid;
using RecipeBook.Infrastructure.Contexts;
using RecipeBook.Infrastructure.Repositories;
using RecipeBook.Infrastructure.SendGrid;

namespace RecipeBook.Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddRepositories()
            .AddSendGrid();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySql(
                configuration["ConnectionString"]!,
                new MySqlServerVersion(new Version(8, 2, 0)),
                options => options.EnableRetryOnFailure());
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepositoryImp>();
        services.AddScoped<IRecipeRepository, RecipeRepositoryImp>();
        services.AddScoped<ICodeRepository, CodeRepositoryImp>();
        return services;
    }

    private static IServiceCollection AddSendGrid(this IServiceCollection services)
    {
        services.AddScoped<ISendGrid, SendGridImp>();

        return services;
    }
}