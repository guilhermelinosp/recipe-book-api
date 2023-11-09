using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.AutoMapper;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Application.UseCases.Accounts.EmailConfirmation;
using RecipeBook.Application.UseCases.Accounts.ForgotPassword;
using RecipeBook.Application.UseCases.Accounts.ResetPassword;
using RecipeBook.Application.UseCases.Accounts.SignIn;
using RecipeBook.Application.UseCases.Accounts.SignUp;
using RecipeBook.Application.UseCases.Recipes.CreateRecipe;
using RecipeBook.Application.UseCases.Recipes.ReadRecipe;

namespace RecipeBook.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddCryptography()
            .AddTokenization()
            .AddMapper();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        services.AddScoped<IEmailConfirmation, EmailConfirmation>();

        services.AddScoped<ICreateRecipeUseCase, CreateRecipeUseCase>();

        services.AddScoped<IFindRecipesByAccountUseCase, FindRecipesByAccountUseCase>();

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppAutoMapper());
        });

        var mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    private static IServiceCollection AddCryptography(this IServiceCollection services)
    {
        services.AddScoped<IEncryptService, EncryptService>();

        return services;
    }

    private static IServiceCollection AddTokenization(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}