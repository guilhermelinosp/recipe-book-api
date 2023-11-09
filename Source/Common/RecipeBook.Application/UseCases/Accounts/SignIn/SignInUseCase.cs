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
    private readonly IAccountRepository _repository;
    private readonly IEncryptService _encrypt;
    private readonly ITokenService _token;
    private readonly IConfiguration _configuration;

    public SignInUseCase(IAccountRepository repository, IEncryptService encryptService, ITokenService tokenService, IConfiguration configuration)
    {
        _repository = repository;
        _encrypt = encryptService;
        _token = tokenService;
        _configuration = configuration;
    }

    public async Task<SignInResponse> SignInAsync(SignInRequest input)
    {
        var validator = new SignInValidator();

        var validationResult = await validator.ValidateAsync(input);
        if (!validationResult.IsValid)
            throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(input.Email!);
        if (account is not null)
        {
            if (account.Password != _encrypt.EncryptPassword(input.Password!))
                throw new ExceptionSignIn(new List<string> { ErrorMessages.LOGIN_INVALIDO });

            if (!account.EmailConfirmed)
                throw new ExceptionSignIn(new List<string> { ErrorMessages.EMAIL_NAO_CONFIRMADO });

            return new SignInResponse
            {
                Token = _token.GenerateToken(new IdentityUser
                {
                    Id = account.AccountId.ToString(),
                    PhoneNumber = account.Phone,
                    Email = account.Email,
                }),
                RefreshToken = _token.GenerateRefreshToken(),
                ExpiryDate = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration["Jwt:ExpiryTimeFrame"]!))
            };
        }

        throw new ExceptionSignIn(new List<string> { ErrorMessages.USUARIO_NAO_ENCONTRADO });

    }
}