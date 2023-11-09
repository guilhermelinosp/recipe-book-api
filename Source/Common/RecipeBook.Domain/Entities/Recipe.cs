using RecipeBook.Domain.Enum;

namespace RecipeBook.Domain.Entities;

public class Recipe
{
    public Guid RecipeId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Title { get; set; }
    public Category Category { get; set; }
    public string? MethodPreparation { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
}