using FluentValidation;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Exceptions;
using System.Text.RegularExpressions;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public class SignUpValidator : AbstractValidator<SignUpRequest>
{
    public SignUpValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.NOME_USUARIO_EMBRANCO)
            .Custom((name, validator) =>
            {
                const string namePattern = @"^[a-zA-Z\s]*$";

                if (!Regex.IsMatch(name!, namePattern))
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(SignUpRequest.Name), ErrorMessages.NOME_USUARIO_INVALIDO));
                }
            });

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_EMBRANCO)
            .EmailAddress()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);


        RuleFor(c => c.Phone)
            .NotEmpty()
            .WithMessage(ErrorMessages.TELEFONE_USUARIO_EMBRANCO)
            .Custom((phone, validator) =>
            {
                const string phonePattern = @"^\d{11}$";

                if (!Regex.IsMatch(phone!, phonePattern))
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(SignUpRequest.Phone), ErrorMessages.TELEFONE_USUARIO_INVALIDO));
                }
            });

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
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(SignUpRequest.Password), ErrorMessages.SENHA_USUARIO_INVALIDA));
                }
            });
    }
}