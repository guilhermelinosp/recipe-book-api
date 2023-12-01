namespace RecipeBook.Application.UseCases.WebSockets.AcceptConnection;

public interface IAcceptConnectionUseCase
{
    Task<Guid> ExecuteAsync(string connectionId, string token);
}