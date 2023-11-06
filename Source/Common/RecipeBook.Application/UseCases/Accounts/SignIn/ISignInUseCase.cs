using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Dtos.Responses;

namespace RecipeBook.Application.UseCases.Accounts.SignIn;

public interface ISignInUseCase
{
    Task<AuthResponse> SignInAsync(SignInRequest input);

}