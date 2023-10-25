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
            RuleFor(c => c.Phone).CustomAsync(async (phone, validator, cancellationToken) =>
            {
                var validationTask = IsPhoneValidAsync(phone, cancellationToken);
                var completedTask = await Task.WhenAny(validationTask, Task.Delay(1000, cancellationToken));

                if (completedTask == validationTask && !validationTask.Result)
                {
                    validator.AddFailure(new FluentValidation.Results.ValidationFailure(phone, ErrorMessages.TELEFONE_USUARIO_INVALIDO));
                }
            });
        });
    }

    private async Task<bool> IsPhoneValidAsync(string? phone, CancellationToken cancellationToken)
    {
        if (phone == null)
        {
            return false; // or handle the null case as needed
        }

        const string phonePattern = @"^\d{11}$";
        return await Task.Run(() => Regex.IsMatch(phone, phonePattern), cancellationToken);
    }
}