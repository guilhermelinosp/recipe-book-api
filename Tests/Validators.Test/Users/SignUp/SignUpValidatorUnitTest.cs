﻿using FluentAssertions;
using RecipeBook.Application.UseCases.Users.SignUp;
using RecipeBook.Exceptions;
using Utils.Test.Requests;
using Xunit;

namespace Validators.Test.Users.SignUp;

public class SignUpValidatorUnitTest
{
    [Fact]
    public async Task ShouldReturnErrorWhenNameIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Name = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.NOME_USUARIO_EMBRANCO));
    }


    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Email = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_EMBRANCO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPhoneIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Phone = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.TELEFONE_USUARIO_EMBRANCO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsEmpty()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Password = string.Empty;

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_EMBRANCO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenEmailIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Email = "invalid_email";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.EMAIL_USUARIO_INVALIDO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPasswordIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Password = "invalid_password";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.SENHA_USUARIO_INVALIDO));
    }

    [Fact]
    public async Task ShouldReturnErrorWhenPhoneIsInvalid()
    {
        var validator = new SignUpValidator();

        var request = RequestSignUpBuilder.Build();

        request.Phone = "invalid_phone";

        var result = await validator.ValidateAsync(request);

        result.IsValid.Should().BeFalse();

        result.Errors.Should().Contain(er => er.ErrorMessage.Equals(ErrorMessages.TELEFONE_USUARIO_INVALIDO));
    }
}