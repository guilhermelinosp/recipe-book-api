namespace RecipeBook.Application.UseCases.Recipes.DeleteRecipe;

public interface IDeleteRecipeUseCase
{
    Task DeleteRecipeByIdAsync(string token, Guid recipeId);
}