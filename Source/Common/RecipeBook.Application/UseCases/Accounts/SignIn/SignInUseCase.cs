using Microsoft.AspNetCore.Identity;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Dtos.Responses;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignIn;

public class SignInUseCase : ISignInUseCase
{
    private readonly IAccountRepository _repository;
    private readonly IEncryptService _encrypt;
    private readonly ITokenService _token;

    public SignInUseCase(IAccountRepository repository, IEncryptService encryptService, ITokenService tokenService)
    {
        _repository = repository;
        _encrypt = encryptService;
        _token = tokenService;
    }

    public async Task<AuthResponse> SignInAsync(SignInRequest input)
    {
        var validator = new SignInValidator();

        var validationResult = await validator.ValidateAsync(input);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var account = await _repository.GetByEmailAsync(input.Email!);
        if (account == null) throw new ExceptionSignIn(ErrorMessages.LOGIN_INVALIDO);

        var password = _encrypt.EncryptPassword(input.Password!);
        if (account.Password != password) throw new ExceptionSignIn(ErrorMessages.LOGIN_INVALIDO);

        var token = _token.GenerateToken(new IdentityUser
        {
            Id = account.Id.ToString(),
            PhoneNumber = account.Phone,
            Email = account.Email,
        });

        return new AuthResponse
        {
            Token = token,
            RefreshToken = "",
            ExpiryDate = DateTime.Now
        };
    }
}