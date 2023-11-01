using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Dtos.Responses;

namespace RecipeBook.Application.UseCases.Users.SignIn;

public interface ISignInUseCase
{
    Task<SignInResponse> ExecuteAsync(SignInRequest input);

}