using AutoMapper;
using MeuLivroDeReceitas.Exceptions;
using RecipeBook.Application.Services.Cryptography;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Users.SignUp;

public class SignUpUseCase : ISignUpUseCase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly EncryptController _encryptController;


    public SignUpUseCase(IUserRepository repository, IMapper mapper, EncryptController encryptController)
    {
        _repository = repository;
        _mapper = mapper;
        _encryptController = encryptController;
    }

    public async Task ExecuteAsync(SignUpRequest input)
    {
        var validator = new SignUpValidator();

        var validationResult = await validator.ValidateAsync(input);
        if (!validationResult.IsValid)
        {
            throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());
        }

        var validateEmail = await _repository.GetByEmailAsync(input.Email!);
        if (validateEmail is not null)
        {
            throw new ExceptionEmailSignUp(ErrorMessages.EMAIL_JA_REGISTRADO);
        }

        var validatePhone = await _repository.GetByPhoneAsync(input.Phone!);
        if (validatePhone is not null)
        {
            throw new ExceptionPhoneSignUp(ErrorMessages.TELEFONE_JA_REGISTRADO);
        }

        input.Password = _encryptController.EncryptPassword(input.Password!);

        await _repository.CreateAsync(_mapper.Map<User>(input));

        await _repository.SaveChangesAsync();
    }
}