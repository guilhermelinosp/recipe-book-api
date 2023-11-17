using RecipeBook.Domain.Dtos.Requests.Recipes;

namespace RecipeBook.Application.UseCases.Recipes.CreateRecipe;

public interface ICreateRecipeUseCase
{
    Task CreateRecipeAsync(string token, CreateRecipeRequest request);
}