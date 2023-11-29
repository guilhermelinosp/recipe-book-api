using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface ICodeRepository
{
    Task CreateCodeAsync(Code code);
    Task<Code> FindCodeByAccountIdAsync(Guid accountId);
    Task<Code> FindCodeByCodeValueAsync(string codeValue);
    Task DeleteCodeAsync(Guid accountId);
}