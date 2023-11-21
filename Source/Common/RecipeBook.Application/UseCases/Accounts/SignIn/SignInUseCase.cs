using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests.Account;
using RecipeBook.Domain.Dtos.Responses.Account;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignIn;

public class SignInUseCase : ISignInUseCase
{
    private readonly IConfiguration _configuration;
    private readonly IEncryptService _encrypt;
    private readonly IAccountRepository _repository;
    private readonly ITokenService _token;

    public SignInUseCase(IAccountRepository repository, IEncryptService encryptService, ITokenService tokenService,
        IConfiguration configuration)
    {
        _repository = repository;
        _encrypt = encryptService;
        _token = tokenService;
        _configuration = configuration;
    }

    public async Task<SignInResponse> SignInAsync(SignInRequest request)
    {
        var validator = await new SignInValidator().ValidateAsync(request);
        if (!validator.IsValid)
            throw new ValidatorException(validator.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(request.Email!);
        if (account is null)
            throw new AccountSignInException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        if (account.Password != _encrypt.EncryptPassword(request.Password!))
            throw new AccountSignInException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_ENCONTRADO });
        if (!account.EmailConfirmed)
            throw new AccountSignInException(new List<string> { ErrorMessages.EMAIL_USUARIO_NAO_CONFIRMADO });

        return new SignInResponse
        {
            Token = _token.GenerateToken(new IdentityUser
            {
                Id = account.AccountId.ToString(),
                PhoneNumber = account.Phone,
                Email = account.Email
            }),
            RefreshToken = _token.GenerateRefreshToken(),
            ExpiryDate = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt-Expiry"]!, CultureInfo.InvariantCulture))
        };
    }
}