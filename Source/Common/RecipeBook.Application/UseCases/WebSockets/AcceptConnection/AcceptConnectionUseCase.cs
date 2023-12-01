using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Repositories;

namespace RecipeBook.Application.UseCases.WebSockets.AcceptConnection;

public class AcceptConnectionUseCase : IAcceptConnectionUseCase
{
    private readonly IConnectionRepository _repository;
    private readonly ITokenService _token;

    public AcceptConnectionUseCase(IConnectionRepository repository, ITokenService token)
    {
        _repository = repository;
        _token = token;
    }

    public async Task<Guid> ExecuteAsync(string connectionId, string token)
    {
        var accountId = _token.ValidateToken(token);

        await _repository.DeleteConnectionAsync(accountId);

        await _repository.CreateConnectionAsync(accountId, new Guid(connectionId));

        await _repository.CreateConnectionAsync(new Guid(connectionId), accountId);

        return accountId;
    }
}