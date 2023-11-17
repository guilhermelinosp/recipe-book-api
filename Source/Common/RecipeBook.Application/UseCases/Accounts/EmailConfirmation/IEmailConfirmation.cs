using RecipeBook.Domain.Dtos.Requests.Account;

namespace RecipeBook.Application.UseCases.Accounts.EmailConfirmation;

public interface IEmailConfirmation
{
    Task EmailConfirmationAsync(EmailConfirmationRequest request);
}