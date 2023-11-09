using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Dtos.Responses.Account;

namespace RecipeBook.Application.UseCases.Accounts.SignIn;

public interface ISignInUseCase
{
    Task<SignInResponse> SignInAsync(SignInRequest request);

}