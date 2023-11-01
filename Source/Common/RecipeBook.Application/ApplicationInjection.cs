﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.AutoMapper;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Application.UseCases.Users.SignIn;
using RecipeBook.Application.UseCases.Users.SignUp;

namespace RecipeBook.Application;

public static class ApplicationInjection
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

        services.AddScoped<ISignInUseCase, SignInUseCase>();

        return services;
    }

    private static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(provider => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperService());
        }).CreateMapper());

        return services;
    }

    private static IServiceCollection AddCryptography(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => new EncryptService(configuration["SecurityKey"]!));

        return services;
    }

    private static IServiceCollection AddTokenization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => new TokenService(configuration["TokenSecurity"]!, double.Parse(configuration["TokenExpiration"]!)));

        return services;
    }
}