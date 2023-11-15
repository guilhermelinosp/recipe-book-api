using RecipeBook.Domain.Dtos.Requests.Ingredients;

namespace RecipeBook.Domain.Dtos.Requests.Recipes;

public class CreateRecipeRequest
{
    public string Title { get; set; }
    public int Category { get; set; }
    public string PreparationMode { get; set; }
    public int PreparationTime { get; set; }
    public List<IngredientRequest> Ingredients { get; set; } = new();
}

