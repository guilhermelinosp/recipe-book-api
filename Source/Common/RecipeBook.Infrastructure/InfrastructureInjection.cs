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
        services.AddDbContext<RecipeBookDbContext>(opt =>
        {
            opt.UseMySql(
                configuration["ConnectionString"]!,
                new MySqlServerVersion(new Version(8, 2, 0)),
                options => options.EnableRetryOnFailure());
        });

        services.AddScoped<IAccountRepository, AccountRepositoryImp>();
        services.AddScoped<IRecipeRepository, RecipeRepositoryImp>();
        services.AddScoped<ICodeRepository, CodeRepositoryImp>();
        services.AddScoped<IConnectionRepository, ConnectionRepositoryImp>();
        services.AddScoped<ISendGrid, SendGridImp>();

        return services;
    }
}