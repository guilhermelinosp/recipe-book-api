using FluentAssertions;
using RecipeBook.Application.UseCases.Accounts.SignIn;
using RecipeBook.Exceptions;
using Utils.Requests;
using Xunit;

namespace Validators.Test.Accounts.SignIn;

public class SignInUnitTest
{
    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsEmpty()
    {
        var validator = new SignInValidator();

        var request = SignInBuilderRequest.Build();

        request.Email = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_EMBRANCO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsEmpty()
    {
        var validator = new SignInValidator();

        var request = SignInBuilderRequest.Build();

        request.Password = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_EMBRANCO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsInvalid()
    {
        var validator = new SignInValidator();

        var request = SignInBuilderRequest.Build();

        request.Email = "invalid_email";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsInvalid()
    {
        var validator = new SignInValidator();

        var request = SignInBuilderRequest.Build();

        request.Password = "invalid_password";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_INVALIDA));
    }
}