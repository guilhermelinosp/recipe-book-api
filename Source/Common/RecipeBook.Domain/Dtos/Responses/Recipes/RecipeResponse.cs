using RecipeBook.Domain.Dtos.Responses.Ingredients;

namespace RecipeBook.Domain.Dtos.Responses.Recipes;

public class RecipeResponse
{
    public string Title { get; set; }
    public int Category { get; set; }
    public string PreparationMode { get; set; }
    public int PreparationTime { get; set; }
    public List<IngredientResponse> Ingredients { get; set; }
}