using RecipeBook.Domain.Dtos.Requests.Account;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public interface IResetPasswordUseCase
{
    Task ResetPasswordAsync(ResetPasswordRequest request);
}