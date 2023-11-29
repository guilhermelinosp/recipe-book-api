using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Domain.SendGrid;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IEncryptService _encrypt;
    private readonly IAccountRepository _repository;
    private readonly ISendGrid _sendGrid;

    public SignUpUseCase(IAccountRepository repository, IEncryptService encrypt, ISendGrid sendGrid)
    {
        _repository = repository;
        _encrypt = encrypt;
        _sendGrid = sendGrid;
    }

    public async Task SignUpAsync(SignUpRequest request)
    {
        var validator = new SignUpValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidatorException(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var validateEmail = await _repository.GetByEmailAsync(request.Email!);
        if (validateEmail is not null)
            throw new AccountException(new List<string> { ErrorMessages.EMAIL_USUARIO_JA_REGISTRADO });

        var emailConfirmationCode = _encrypt.GenerateCode();

        await _repository.CreateAsync(new Account
        {
            Name = request.Name!,
            Email = request.Email!,
            Code = emailConfirmationCode,
            Password = _encrypt.EncryptPassword(request.Password!)
        });

        await _sendGrid.SendConfirmationEmailAsync(request.Email!, request.Name!, emailConfirmationCode);
    }
}