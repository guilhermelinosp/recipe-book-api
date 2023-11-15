namespace RecipeBook.Domain.Dtos.Requests.Recipes;

public class FindRecipeRequest
{
    public string Title { get; set; }
    public int Category { get; set; }
}