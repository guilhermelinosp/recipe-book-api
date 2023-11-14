using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities;

[Table("TB_Ingredient")]
public class Ingredient
{
<<<<<<< HEAD
    [Key] public Guid IngredientId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Product { get; set; }
    public string Quantity { get; set; }
=======
    public Guid IngredientId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Product { get; set; }
    public int Quantity { get; set; }
>>>>>>> 5e0ad8c7692647508c252071f6c986e6f55a96d0
    public Guid RecipeId { get; set; }
}