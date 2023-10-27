using FluentValidation;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Exceptions;
using System.Text.RegularExpressions;

namespace RecipeBook.Application.UseCases.Users.SignUp;

public class SignUpValidator : AbstractValidator<RequestSignUp>
{
    public SignUpValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.NOME_USUARIO_EMBRANCO);

        RuleFor(c => c.Email)
            .NotEmpty()
            .WithMessage(ErrorMessages.EMAIL_USUARIO_EMBRANCO);

        RuleFor(c => c.Phone)
            .NotEmpty()
            .WithMessage(ErrorMessages.TELEFONE_USUARIO_EMBRANCO);

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

        When(c => !string.IsNullOrWhiteSpace(c.Phone), () =>
        {
            RuleFor(c => c.Phone).CustomAsync(async (phone, validator, cancellationToken) =>
            {
                if (phone == null)
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(RequestSignUp.Phone), ErrorMessages.TELEFONE_USUARIO_INVALIDO));
                    return;
                }

                const string phonePattern = @"^\d{11}$";

                var phoneValidationTask = Task.Run(() => Regex.IsMatch(phone, phonePattern));

                if (await Task.WhenAny(phoneValidationTask, Task.Delay(1000, cancellationToken)) == phoneValidationTask)
                {
                    if (!phoneValidationTask.Result)
                    {
                        validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(RequestSignUp.Phone), ErrorMessages.TELEFONE_USUARIO_INVALIDO));
                    }
                }
                else
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(RequestSignUp.Phone), "Phone validation timed out."));
                }
            });
        });
    }
}