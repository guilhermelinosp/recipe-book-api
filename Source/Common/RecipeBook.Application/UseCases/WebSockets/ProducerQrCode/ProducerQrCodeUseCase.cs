using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Responses.WebSockets;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;

namespace RecipeBook.Application.UseCases.WebSockets.ProducerQrCode;

public class ProducerQrCodeUseCase : IProducerQrCodeUseCase
{
    private readonly ICodeRepository _code;
    private readonly ITokenService _token;

    public ProducerQrCodeUseCase(ICodeRepository code, ITokenService token)
    {
        _code = code;
        _token = token;
    }

    public async Task<ProducerQrCodeResponse> ExecuteAsync(string token)
    {
        var accountId = _token.ValidateToken(token);

        var code = new Code
        {
            AccountId = accountId,
            CodeValue = Guid.NewGuid().ToString()
        };

        await _code.CreateCodeAsync(code);

        return new ProducerQrCodeResponse
        {
            Code = code.CodeValue,
            AccountId = code.AccountId
        };
    }
}