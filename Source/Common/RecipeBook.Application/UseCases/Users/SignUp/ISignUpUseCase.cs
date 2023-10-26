using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Users.SignUp;

public interface ISignUpUseCase
{
    Task ExecuteAsync(RequestSignUp input);
}