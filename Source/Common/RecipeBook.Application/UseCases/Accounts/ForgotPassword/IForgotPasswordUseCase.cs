using RecipeBook.Domain.Dtos.Requests.Account;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword;

public interface IForgotPasswordUseCase
{
    Task ForgoPasswordAsync(ForgotPasswordRequest request);
}