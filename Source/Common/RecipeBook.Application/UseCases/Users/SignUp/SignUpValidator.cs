using FluentValidation;
using MeuLivroDeReceitas.Exceptions;
using RecipeBook.Domain.Dtos.Requests;
using System.Text.RegularExpressions;

namespace RecipeBook.Application.UseCases.Users.SignUp;

public class SignUpValidator : AbstractValidator<SignUpRequest>
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
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.")
            .Matches("[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("Password must contain at least one special character.");

        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage(ErrorMessages.EMAIL_USUARIO_INVALIDO);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Phone), () =>
        {
            RuleFor(c => c.Phone).Custom((phone, validator) =>
            {
                const string phonePattern = @"^\d{11}$";

                var isMatchTask = Task.Run(() => Regex.IsMatch(phone!, phonePattern));

                if (isMatchTask.Wait(TimeSpan.FromMilliseconds(1000)) && !isMatchTask.Result)
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(phone, ErrorMessages.TELEFONE_USUARIO_INVALIDO));
                }
            });
        });

    }
}