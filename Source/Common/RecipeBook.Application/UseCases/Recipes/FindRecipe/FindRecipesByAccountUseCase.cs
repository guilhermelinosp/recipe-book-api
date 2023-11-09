using RecipeBook.Application.Services.Tokenization;
using RecipeBook.Domain.Dtos.Requests.Recipes;
using RecipeBook.Domain.Dtos.Responses.Ingredients;
using RecipeBook.Domain.Dtos.Responses.Recipes;
using RecipeBook.Domain.Entities;
using RecipeBook.Domain.Repositories;
using System.Globalization;
using System.Text;

namespace RecipeBook.Application.UseCases.Recipes.ReadRecipe;

public class FindRecipesByAccountUseCase : IFindRecipesByAccountUseCase
{
    private readonly IRecipeRepository _repository;
    private readonly ITokenService _token;

    public FindRecipesByAccountUseCase(IRecipeRepository repository, ITokenService token)
    {
        _repository = repository;
        _token = token;
    }

    public async Task<IEnumerable<RecipeResponse>?> FindRecipesAsync(string token, FindRecipeRequest? request)
    {
        var id = _token.GetIdFromToken(token);

        var recipes = await _repository.FindRecipesByAccountIdAsync(id);

        var filteredRecipes = new List<Recipe>(recipes);

        if (request?.Category != null)
        {
            filteredRecipes = filteredRecipes
                .Where(r => r.Category == request.Category)
                .ToList();
        }

        if (!string.IsNullOrWhiteSpace(request?.Title))
        {
            var searchTerm = NormalizeAndLowercase(request.Title);
            filteredRecipes = filteredRecipes
                .Where(r => NormalizeAndLowercase(r.Title).Contains(searchTerm) ||
                            r.Ingredients.Any(ingredient => NormalizeAndLowercase(ingredient.Product).Contains(searchTerm)))
                .ToList();
        }

        return filteredRecipes.OrderBy(r => r.Title).Select(r => new RecipeResponse
        {
            Title = r.Title,
            Category = r.Category,
            PreparationMode = r.PreparationMode,
            Ingredients = r.Ingredients.Select(i => new IngredientResponse
            {
                Product = i.Product,
                Quantity = i.Quantity,
            }).ToList()
        });
    }

    private static string NormalizeAndLowercase(string input)
    {
        return new string(input
                .Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
            .ToLowerInvariant();
    }




}