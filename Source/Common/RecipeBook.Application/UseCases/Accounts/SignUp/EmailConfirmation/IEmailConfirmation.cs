using RecipeBook.Domain.Dtos.Requests;

namespace RecipeBook.Application.UseCases.Accounts.SignUp.EmailConfirmation;

public interface IEmailConfirmation
{
    Task EmailConfirmationAsync(EmailConfirmationRequest request);
}