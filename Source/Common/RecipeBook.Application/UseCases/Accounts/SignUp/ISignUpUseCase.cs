using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public interface ISignUpUseCase
{
    Task SignUpAsync(SignUpRequest request);

}