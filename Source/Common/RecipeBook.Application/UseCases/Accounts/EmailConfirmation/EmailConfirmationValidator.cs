using FluentValidation;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.EmailConfirmation;

public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationRequest>
{
    public EmailConfirmationValidator()
    {
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage(ErrorMessages.CODIGO_INVALIDO)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.CODIGO_INVALIDO)
            .MaximumLength(6)
            .WithMessage(ErrorMessages.CODIGO_INVALIDO);
    }
}