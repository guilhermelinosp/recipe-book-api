using AutoMapper;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.SignUp;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptService _encryptService;

    public SignUpUseCase(IAccountRepository repository, IMapper mapper, EncryptService encryptService)
    {
        _repository = repository;
        _mapper = mapper;
        _encryptService = encryptService;
    }

    public async Task SignUpAsync(SignUpRequest request)
    {
        var validator = new SignUpValidator();

        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var validateEmail = await _repository.GetByEmailAsync(request.Email!);
        if (validateEmail is not null) throw new ExceptionSignUp(new List<string> { ErrorMessages.EMAIL_JA_REGISTRADO });

        var validatePhone = await _repository.GetByPhoneAsync(request.Phone!);
        if (validatePhone is not null) throw new ExceptionSignUp(new List<string> { ErrorMessages.TELEFONE_JA_REGISTRADO });

        request.Password = _encryptService.EncryptPassword(request.Password!);

        await _repository.CreateAsync(_mapper.Map<Account>(request));

        await _repository.SaveChangesAsync();
    }
}