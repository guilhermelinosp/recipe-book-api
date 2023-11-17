using RecipeBook.Domain.Dtos.Requests.Account;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public interface ISignUpUseCase
{
    Task SignUpAsync(SignUpRequest request);
}