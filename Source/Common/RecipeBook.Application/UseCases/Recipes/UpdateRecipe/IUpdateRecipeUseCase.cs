using RecipeBook.Domain.Dtos.Requests.Recipes;

namespace RecipeBook.Application.UseCases.Recipes.UpdateRecipe;

public interface IUpdateRecipeUseCase
{
    Task UpdateRecipeAsync(string token, Guid receitaId, UpdateRecipeRequest request);
}