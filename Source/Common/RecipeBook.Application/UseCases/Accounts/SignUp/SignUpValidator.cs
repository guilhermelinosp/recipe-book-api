using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public partial class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.NOME_USUARIO_NAO_INFORMADO)
            .Custom((name, validator) =>
            {
                if (!RegexName().IsMatch(name))
                    validator.AddFailure(new ValidationFailure(nameof(SignUpRequest.Name),
                        ErrorMessages.NOME_USUARIO_INVALIDO));
            });

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_NAO_INFORMADO)
            .EmailAddress()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);

        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.SENHA_USUARIO_NAO_INFORMADO)
            .MinimumLength(8)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                if (!RegexPasswoed().IsMatch(password))
                    validator.AddFailure(new ValidationFailure(nameof(SignUpRequest.Password),
                        ErrorMessages.SENHA_USUARIO_INVALIDA));
            });
    }

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
    private static partial Regex RegexPasswoed();

    [GeneratedRegex(@"^[a-zA-Z\s]*$")]
    private static partial Regex RegexName();
}