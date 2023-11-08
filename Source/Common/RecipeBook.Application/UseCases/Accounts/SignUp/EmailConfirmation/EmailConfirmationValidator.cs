using FluentValidation;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignUp.EmailConfirmation;

public class EmailConfirmationValidator : AbstractValidator<EmailConfirmationRequest>
{
    public EmailConfirmationValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO)
            .EmailAddress()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);


        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage(ErrorMessages.CODIGO_INVALIDO)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.CODIGO_INVALIDO)
            .MaximumLength(6)
            .WithMessage(ErrorMessages.CODIGO_INVALIDO);
    }
}