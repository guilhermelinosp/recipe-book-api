namespace RecipeBook.Domain.Entities;

public class Ingredient
{
    public Guid IngredientId { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid RecipeId { get; set; }
    public Recipe Recipe { get; set; }
}