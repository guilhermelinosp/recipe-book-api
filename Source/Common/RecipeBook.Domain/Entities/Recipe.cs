namespace RecipeBook.Domain.Entities;

public class Recipe
{
    public Guid RecipeId { get; set; }
    public string Title { get; set; }
    public int Category { get; set; }
    public string MethodPreparation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Ingredient> Ingredients { get; set; }
}