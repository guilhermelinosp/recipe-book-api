using FluentValidation;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_EMBRANCO)
            .EmailAddress()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);
    }
}