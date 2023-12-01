namespace RecipeBook.Application.UseCases.WebSockets.RefuseConnection;

public interface IRefuseConnectionUseCase
{
    Task<Guid> ExecuteAsync(string token);
}