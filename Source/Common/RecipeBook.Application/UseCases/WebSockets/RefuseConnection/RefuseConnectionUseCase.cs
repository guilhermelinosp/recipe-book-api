using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Repositories;

namespace RecipeBook.Application.UseCases.WebSockets.RefuseConnection;

public class RefuseConnectionUseCase : IRefuseConnectionUseCase
{
    private readonly IConnectionRepository _repository;
    private readonly ITokenService _token;

    public RefuseConnectionUseCase(ITokenService token, IConnectionRepository repository)
    {
        _token = token;
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(string token)
    {
        var accountId = _token.ValidateToken(token);

        await _repository.DeleteConnectionAsync(accountId);

        return accountId;
    }
}