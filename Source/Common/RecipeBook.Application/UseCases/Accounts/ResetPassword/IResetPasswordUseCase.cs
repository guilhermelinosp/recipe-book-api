using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public interface IResetPasswordUseCase
{
    Task ResetPasswordAsync(ResetPasswordRequest input);

}