using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword.ResetPassword;

public interface IResetPasswordUseCase
{
    Task ResetPasswordAsync(ResetPasswordRequest request);
}