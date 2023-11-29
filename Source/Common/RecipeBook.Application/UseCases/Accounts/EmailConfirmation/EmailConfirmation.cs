using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.EmailConfirmation;

public class EmailConfirmation : IEmailConfirmation
{
    private readonly IAccountRepository _repository;

    public EmailConfirmation(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task EmailConfirmationAsync(EmailConfirmationRequest request)
    {
        var validator = new EmailConfirmationValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByCodeAsync(request.Code!);
        if (account is not null)
        {
            if (account.Auth)
                throw new AccountException(new List<string>
                    { "The email has already been confirmed previously" });

            if (account.Code != request.Code)
                throw new AccountException(new List<string> { ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO });

            account.Auth = true;
            account.Code = string.Empty;

            await _repository.UpdateAsync(account);
        }
        else
        {
            throw new AccountException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        }
    }
}