using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Responses.Ingredients;
using RecipeBook.Domain.Dtos.Responses.Recipes;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.FindRecipeById;

public class FindRecipeByIdUseCase : IFindRecipeByIdUseCase
{
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;

    public FindRecipeByIdUseCase(IRecipeRepository repository, ITokenService token)
    {
        _repository = repository;
        _token = token;
    }

    public async Task<RecipeResponse> FindRecipeByRecipeIdAsync(string token, Guid recipeId)
    {
        var accountId = _token.GetIdFromToken(token);

        var recipe = await _repository.FindRecipeByIdAsync(recipeId, accountId);

        if (recipe is null) throw new RecipeException(new List<string> { ErrorMessages.RECEITA_NAO_ENCONTRADO });

        return new RecipeResponse
        {
            Category = recipe.Category,
            PreparationMode = recipe.PreparationMode,
            PreparationTime = recipe.PreparationTime,
            RecipeId = recipe.RecipeId,
            Title = recipe.Title,
            Ingredients = recipe.Ingredients.Select(i => new IngredientResponse
            {
                Product = i.Product,
                Quantity = i.Quantity
            }).ToList()
        };
    }
}