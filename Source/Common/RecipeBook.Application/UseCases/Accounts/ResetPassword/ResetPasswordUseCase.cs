using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ResetPassword;

public class ResetPasswordUseCase : IResetPasswordUseCase
{
    private readonly IAccountRepository _repository;
    private readonly IEncryptService _encrypt;

    public ResetPasswordUseCase(IAccountRepository repository, IEncryptService encrypt)
    {
        _repository = repository;
        _encrypt = encrypt;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var validator = new ResetPasswordValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByCodeAsync(request.Code!);
        if (account is null) throw new ExceptionResetPassword(new List<string> { ErrorMessages.CODIGO_INVALIDO });
        account.Password = _encrypt.EncryptPassword(request.Password!);

        account.Code = string.Empty;

        await _repository.UpdateAsync(account);
    }
}