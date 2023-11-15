using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.FindRecipeByRecipeId;

public class FindRecipeByRecipeIdUseCase : IFindRecipeByRecipeIdUseCase
{
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;

    public FindRecipeByRecipeIdUseCase(IRecipeRepository repository, ITokenService token)
    {
        _repository = repository;
        _token = token;
    }

    public async Task<Recipe> FindRecipeByRecipeIdAsync(string token, Guid recipeId)
    {
        var accountId = _token.GetIdFromToken(token);

        var recipe = await _repository.FindRecipeByRecipeIdAsync(recipeId, accountId);

        if (recipe is null) throw new RecipeNotFoundException("");

        return new Recipe();
    }
}