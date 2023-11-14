using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Recipes;

namespace RecipeBook.Application.UseCases.Recipes.FindRecipe;

public interface IFindRecipesByAccountUseCase
{
    public Task<IEnumerable<RecipeResponse>?> FindRecipesAsync(string token, FindRecipeRequest? request);
}