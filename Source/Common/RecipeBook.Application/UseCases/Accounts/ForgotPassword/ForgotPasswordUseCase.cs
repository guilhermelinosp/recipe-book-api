using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Repositories;
using RecipeBook.Domain.SendGrid;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword;

public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IAccountRepository _repository;
    private readonly IEncryptService _encrypt;
    private readonly ISendGrid _sendGrid;


    public ForgotPasswordUseCase(IAccountRepository repository, IEncryptService encrypt, ISendGrid sendGrid)
    {
        _repository = repository;
        _encrypt = encrypt;
        _sendGrid = sendGrid;
    }

    public async Task ForgoPasswordAsync(ForgotPasswordRequest request)
    {

        var validator = new ForgotPasswordValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(request.Email!);

        if (account is null)
            throw new ExceptionForgotPassword(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

        var code = _encrypt.GenerateCode();

        account.Code = code;

        await _repository.UpdateAsync(account);

        await _sendGrid.SendForgotPasswordEmailAsync(request.Email!, account.Name!, code);
    }
}