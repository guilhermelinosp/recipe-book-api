using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Responses.WebSockets;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.WebSockets.ConsumerQrCode;

public class ConsumerQrCodeUseCase : IConsumerQrCodeUseCase
{
    private readonly IAccountRepository _account;
    private readonly ICodeRepository _code;
    private readonly ITokenService _token;


    public ConsumerQrCodeUseCase(ICodeRepository code, ITokenService token, IAccountRepository account)
    {
        _code = code;
        _token = token;
        _account = account;
    }

    public async Task<ConsumerQrCodeResponse> ExecuteAsync(string codeValue, string token)
    {
        var accountId = _token.ValidateToken(token);

        var code = await _code.FindCodeByCodeValueAsync(codeValue);
        if (code is null)
            throw new WebSocketException(new List<string> { "Code not found" });

        if (code.AccountId == accountId)
            throw new WebSocketException(new List<string> { "Account is equal" });

        var account = await _account.GetByIdAsync(code.AccountId);

        await _code.DeleteCodeAsync(code.AccountId);

        return new ConsumerQrCodeResponse
        {
            Name = account!.Name,
            AccountId = code.AccountId
        };
    }
}