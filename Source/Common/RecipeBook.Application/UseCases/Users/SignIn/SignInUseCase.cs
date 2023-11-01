using AutoMapper;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Dtos.Responses;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Users.SignIn;

public class SignInUseCase : ISignInUseCase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptService _encryptService;
    private readonly TokenService _tokenService;

    public SignInUseCase(IUserRepository repository, IMapper mapper, EncryptService encryptService, TokenService tokenService)
    {
        _repository = repository;
        _mapper = mapper;
        _encryptService = encryptService;
        _tokenService = tokenService;
    }

    public async Task<SignInResponse> ExecuteAsync(SignInRequest input)
    {
        var validator = new SignInValidator();

        var validationResult = await validator.ValidateAsync(input);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var user = await _repository.GetByEmailAsync(input.Email!);

        if (user == null) throw new ExceptionSignIn(ErrorMessages.LOGIN_INVALIDO);

        var password = _encryptService.EncryptPassword(input.Password!);

        if (user.Password != password) throw new ExceptionSignIn(ErrorMessages.LOGIN_INVALIDO);

        var token = _tokenService.GenerateToken(user);

        return new SignInResponse
        {
            Token = token
        };
    }
}