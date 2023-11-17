using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Repositories;
using RecipeBook.Exceptions;
using RecipeBook.Exceptions.Exceptions;

namespace RecipeBook.Application.UseCases.Recipes.DeleteRecipe;

public class DeleteRecipeUseCase : IDeleteRecipeUseCase
{
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;

    public DeleteRecipeUseCase(IRecipeRepository repository, ITokenService token)
    {
        _repository = repository;
        _token = token;
    }

    public async Task DeleteRecipeByIdAsync(string token, Guid recipeId)
    {
        var accountId = _token.GetIdFromToken(token);

        var recipe = await _repository.FindRecipeByIdAsync(recipeId, accountId);

        if (recipe is null) throw new RecipeException(new List<string> { ErrorMessages.RECEITA_NAO_ENCONTRADO });

        await _repository.DeleteRecipeAsync(recipe);
    }
}