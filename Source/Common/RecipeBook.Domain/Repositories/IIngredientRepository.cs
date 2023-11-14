using RecipeBook.Domain.Entities;

namespace RecipeBook.Domain.Repositories;

public interface IIngredientRepository
{
    Task<Ingredient> GetIngredientAsync(Guid id);
    Task<IEnumerable<Ingredient>> GetIngredientsAsync();
    Task CreateIngredientAsync(Ingredient ingredient);
    Task UpdateIngredientAsync(Ingredient ingredient);
    Task DeleteIngredientAsync(Guid id);

}