using FluentAssertions;
using RecipeBook.Application.UseCases.Users.SignIn;
using RecipeBook.Exceptions.Exceptions;
using Utils.AutoMapper;
using Utils.Cryptography;
using Utils.Repositories;
using Utils.Requests;
using Utils.Tokenization;
using Xunit;

namespace UseCases.Test.Users.SignIn;

public class SignInUseCaseUnitTest
{
    private static SignInUseCase UseCaseBuilder()
    {
        var repository = UserRepositoryBuilder.Instance()!.Build();
        var mapper = AutoMapperBuilder.Instance()!.Build();
        var encryptService = EncryptBuilder.Instance()!.Build();
        var tokenService = TokenBuilder.Instance()!.Build();
        return new SignInUseCase(repository, mapper, encryptService, tokenService);
    }


    [Fact]
    public async Task ShouldReturnSuccessWhenValidRequest()
    {
        var request = SignInRequestBuilder.Build();
        request.Password = "'Mudar@123";

        var useCase = UseCaseBuilder();

        var action = async () => await useCase.ExecuteAsync(request);

        await action.Should().NotThrowAsync<ExceptionValidator>();
    }
}