namespace RecipeBook.Domain.Repositories;

public interface IConnectionRepository
{
    Task<bool> CheckConnectionAsync(Guid accountId, Guid subAccountId);
}