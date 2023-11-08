using Microsoft.Extensions.DependencyInjection;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Application.UseCases.Accounts;
using RecipeBook.Application.UseCases.Accounts.ForgotPassword;
using RecipeBook.Application.UseCases.Accounts.ForgotPassword.ResetPassword;
using RecipeBook.Application.UseCases.Accounts.SignIn;
using RecipeBook.Application.UseCases.Accounts.SignUp;
using RecipeBook.Application.UseCases.Accounts.SignUp.EmailConfirmation;

namespace RecipeBook.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddUseCases()
            .AddCryptography()
            .AddTokenization();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        services.AddScoped<IEmailConfirmation, EmailConfirmation>();
        services.AddScoped(typeof(TestUseCase));

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