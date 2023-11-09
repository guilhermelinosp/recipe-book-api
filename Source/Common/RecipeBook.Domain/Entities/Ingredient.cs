namespace RecipeBook.Domain.Entities;

public class Ingredient
{
    public Guid IngredientId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Product { get; set; }
    public int Quantity { get; set; }
    public Guid RecipeId { get; set; }
}