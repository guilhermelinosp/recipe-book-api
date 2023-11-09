using RecipeBook.Domain.Dtos.Responses.Ingredients;
using RecipeBook.Domain.Enum;

namespace RecipeBook.Domain.Dtos.Responses.Recipes;

public class RecipeResponse
{
    public string Title { get; set; }
    public Category Category { get; set; }
    public string PreparationMode { get; set; }
    public List<IngredientResponse> Ingredients { get; set; }
}