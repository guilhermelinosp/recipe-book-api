using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Accounts.ForgotPassword;

public class ForgotPasswordUseCase : IForgotPasswordUseCase
{
    private readonly IAccountRepository _repository;

    public ForgotPasswordUseCase(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task ForgoPasswordAsync(ForgotPasswordRequest input)
    {
        var validator = new ForgotPasswordValidator();

        var validationResult = await validator.ValidateAsync(input);
        if (!validationResult.IsValid) throw new ExceptionValidator(validationResult.Errors.Select(er => er.ErrorMessage).ToList());

        var user = await _repository.GetByEmailAsync(input.Email!);

        if (user is null) throw new ExceptionForgotPassword(ErrorMessages.USUARIO_NAO_ENCONTRADO);





    }
}