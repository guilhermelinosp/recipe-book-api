using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Results;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public partial class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.SENHA_USUARIO_NAO_INFORMADO)
            .MinimumLength(8)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                if (!MyRegex().IsMatch(password))
                    validator.AddFailure(new ValidationFailure(
                        nameof(ResetPasswordRequest.Password), ErrorMessages.SENHA_USUARIO_INVALIDA));
            });

        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO)
            .MinimumLength(6)
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO)
            .MaximumLength(6)
            .WithMessage(ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO);
    }

    [GeneratedRegex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$")]
    private static partial Regex MyRegex();
}