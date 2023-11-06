using FluentValidation;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Exceptions;
using System.Text.RegularExpressions;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty()
            .WithMessage(ErrorMessages.SENHA_USUARIO_EMBRANCO)
            .MinimumLength(8)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MINIMO_OITO_CARACTERES)
            .MaximumLength(16)
            .WithMessage(ErrorMessages.SENHA_USUARIO_MAXIMO_DEZESSEIS_CARACTERES)
            .Custom((password, validator) =>
            {
                const string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,16}$";

                if (!Regex.IsMatch(password, passwordPattern))
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(
                        nameof(ResetPasswordRequest.Password), ErrorMessages.SENHA_USUARIO_INVALIDA));
                }
            });
    }

}