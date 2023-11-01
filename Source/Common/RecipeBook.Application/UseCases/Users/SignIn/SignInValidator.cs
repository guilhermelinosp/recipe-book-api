using FluentValidation;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Users.SignIn;

public class SignInValidator : AbstractValidator<SignInRequest>
{
    public SignInValidator()
    {
        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_EMBRANCO);


        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.SENHA_USUARIO_EMBRANCO);

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Password), () =>
        {
            RuleFor(c => c.Password)
                .MinimumLength(8)
                .WithMessage(ErrorMessages.SENHA_USUARIO_MINIMO_OITO_CARACTERES)
                .Matches("[A-Z]")
                .WithMessage(ErrorMessages.SENHA_USUARIO_INVALIDO)
                .Matches("[a-z]")
                .WithMessage(ErrorMessages.SENHA_USUARIO_INVALIDO)
                .Matches("[0-9]")
                .WithMessage(ErrorMessages.SENHA_USUARIO_INVALIDO)
                .Matches("[^a-zA-Z0-9]")
                .WithMessage(ErrorMessages.SENHA_USUARIO_INVALIDO);
        });
    }
}