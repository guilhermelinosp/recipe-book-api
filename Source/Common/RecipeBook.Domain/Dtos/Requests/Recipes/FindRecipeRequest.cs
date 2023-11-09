using RecipeBook.Domain.Enum;

namespace RecipeBook.Domain.Dtos.Requests.Recipes;

public class FindRecipeRequest
{
    public string Title { get; set; }
    public Category Category { get; set; }
}