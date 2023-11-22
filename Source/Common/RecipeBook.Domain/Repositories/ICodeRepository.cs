using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface ICodeRepository
{
    Task CreateCodeAsync(Code code);
    Task SaveChangesAsync();

}