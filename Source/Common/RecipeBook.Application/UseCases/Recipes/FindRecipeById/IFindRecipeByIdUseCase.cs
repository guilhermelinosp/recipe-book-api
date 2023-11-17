using RecipeBook.Domain.Dtos.Responses.Recipes;

namespace RecipeBook.Application.UseCases.Recipes.FindRecipeById;

public interface IFindRecipeByIdUseCase
{
    Task<RecipeResponse> FindRecipeByRecipeIdAsync(string token, Guid recipeId);
}