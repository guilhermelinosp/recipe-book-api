﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Application.UseCases.Accounts;
using RecipeBook.Application.UseCases.Accounts.ForgotPassword;
using RecipeBook.Application.UseCases.Accounts.ResetPassword;
using RecipeBook.Application.UseCases.Accounts.SignIn;
using RecipeBook.Application.UseCases.Accounts.SignUp;

namespace RecipeBook.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddUseCases()
            .AddCryptography(configuration)
            .AddTokenization(configuration);

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        services.AddScoped<IAccountUseCase, AccountUseCase>();

        return services;
    }

    private static IServiceCollection AddCryptography(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ => new EncryptService(configuration));

        services.AddScoped<IEncryptService, EncryptService>();

        return services;
    }

    private static IServiceCollection AddTokenization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(_ => new TokenService(configuration));

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}