using FluentAssertions;
using RecipeBook.Application.UseCases.Accounts.SignUp;
using RecipeBook.Exceptions;
using Utils.Requests;
using Xunit;

namespace Validators.Test.Accounts.SignUp;

public class SignUpUnitTest
{
    [Fact]
    public async Task ShouldReturnErrorWhenNameIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Name = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.NOME_USUARIO_NAO_INFORMADO));
    }


    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Email = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_NAO_INFORMADO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPhoneIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Phone = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.TELEFONE_USUARIO_NAO_INFORMADO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Password = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_NAO_INFORMADO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Email = "invalid_email";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Password = "invalid_password";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_INVALIDA));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPhoneIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = SignUpBuilderRequest.Build();

        request.Phone = "invalid_phone";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.TELEFONE_USUARIO_INVALIDO));
    }
}