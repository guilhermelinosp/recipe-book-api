using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword;

public interface IForgotPasswordUseCase
{
    Task ForgoPasswordAsync(ForgotPasswordRequest input);
}