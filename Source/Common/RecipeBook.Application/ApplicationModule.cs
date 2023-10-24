using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.AutoMapper;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Application.UseCases.Users.SignUp;

namespace RecipeBook.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddUseCases()
            .AddAutoMapper()
            .AddCryptography(configuration)
            .AddTokenization(configuration);

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperController());
        }).CreateMapper());

        return services;
    }

    private static IServiceCollection AddCryptography(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => new EncryptController(configuration["SecurityKey"]!));

        return services;
    }

    private static IServiceCollection AddTokenization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => new TokenController(configuration["TokenSecurity"]!, double.Parse(configuration["TokenExpiration"]!)));

        return services;
    }
}