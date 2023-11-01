using FluentAssertions;
using RecipeBook.Application.UseCases.Users.SignUp;
using RecipeBook.Exceptions.Exceptions;
using Utils.AutoMapper;
using Utils.Cryptography;
using Utils.Repositories;
using Utils.Requests;
using Xunit;

namespace UseCases.Test.Users.SignUp;

public class SignUpUseCaseUnitTest
{
    private static SignUpUseCase UseCaseBuilder()
    {
        var repository = UserRepositoryBuilder.Instance()!.Build();
        var mapper = AutoMapperBuilder.Instance()!.Build();
        var encryptService = EncryptBuilder.Instance()!.Build();
        return new SignUpUseCase(repository, mapper, encryptService);
    }


    [Fact]
    public async Task ShouldReturnSuccessWhenValidRequest()
    {
        var request = SignUpRequestBuilder.Build();
        request.Password = "Password@123";

        var useCase = UseCaseBuilder();

        var action = async () => await useCase.ExecuteAsync(request);

        await action.Should().NotThrowAsync<ExceptionValidator>();
    }


    [Fact]
    public async Task ShouldReturnErrorWhenEmailExists()
    {
        var request = SignUpRequestBuilder.Build();

        request.Email = "existthisemail@email.com";

        var useCase = UseCaseBuilder();

        var action = async () => await useCase.ExecuteAsync(request);

        await action.Should().ThrowAsync<ExceptionValidator>();
    }


    [Fact]
    public async Task ShouldReturnErrorWhenPhoneExists()
    {
        var request = SignUpRequestBuilder.Build();

        request.Phone = "01234567890";

        var useCase = UseCaseBuilder();

        var action = async () => await useCase.ExecuteAsync(request);

        await action.Should().ThrowAsync<ExceptionValidator>();
    }
}