using RecipeBook.Domain.Dtos.Requests.Ingredients;
using RecipeBook.Domain.Enum;

namespace RecipeBook.Domain.Dtos.Requests.Recipes;

public class UpdateRecipeRequest
{
    public string Title { get; set; }
    public Category Category { get; set; }
    public string PreparationMode { get; set; }
    public int PreparationTime { get; set; }
    public List<IngredientRequest> Ingredients { get; set; } = new();
}