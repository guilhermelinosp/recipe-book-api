using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public class ResetPasswordUseCase : IResetPasswordUseCase
{
    private readonly IEncryptService _encrypt;
    private readonly IAccountRepository _repository;

    public ResetPasswordUseCase(IAccountRepository repository, IEncryptService encrypt)
    {
        _repository = repository;
        _encrypt = encrypt;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var validator = new ResetPasswordValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByCodeAsync(request.Code!);
        if (account is null)
            throw new AccountResetPasswordException(new List<string> { ErrorMessages.EMAIL_USUARIO_CODIGO_INVALIDO });
        account.Password = _encrypt.EncryptPassword(request.Password!);

        account.Code = string.Empty;

        await _repository.UpdateAsync(account);
    }
}