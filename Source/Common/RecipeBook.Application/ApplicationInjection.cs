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
using RecipeBook.Application.UseCases.Recipes.DeleteRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipe;
using RecipeBook.Application.UseCases.Recipes.FindRecipeById;
using RecipeBook.Application.UseCases.Recipes.UpdateRecipe;
using RecipeBook.Application.UseCases.WebSockets.ConsumerQrCode;
using RecipeBook.Application.UseCases.WebSockets.ProducerQrCode;

namespace RecipeBook.Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ISignUpUseCase, SignUpUseCase>();
        services.AddScoped<ISignInUseCase, SignInUseCase>();
        services.AddScoped<IForgotPasswordUseCase, ForgotPasswordUseCase>();
        services.AddScoped<IResetPasswordUseCase, ResetPasswordUseCase>();
        services.AddScoped<IEmailConfirmation, EmailConfirmation>();

        services.AddScoped<ICreateRecipeUseCase, CreateRecipeUseCase>();
        services.AddScoped<IFindRecipesUseCase, FindRecipesUseCase>();
        services.AddScoped<IFindRecipeByIdUseCase, FindRecipeByIdUseCase>();
        services.AddScoped<IDeleteRecipeUseCase, DeleteRecipeUseCase>();
        services.AddScoped<IUpdateRecipeUseCase, UpdateRecipeUseCase>();

        services.AddScoped<IProducerQrCodeUseCase, ProducerQrCodeUseCase>();
        services.AddScoped<IConsumerQrCodeUseCase, ConsumerQrCodeUseCase>();

        services.AddSingleton(new MapperConfiguration(cfg => { cfg.AddProfile(new AppAutoMapper()); }).CreateMapper());

        services.AddScoped<IEncryptService, EncryptService>();
        services.AddSingleton<ITokenService, TokenService>();
        return services;
    }
}