using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities;

[Table("TB_Ingredient")]
public class Ingredient
{
    [Key] public Guid IngredientId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Product { get; set; }
    public string Quantity { get; set; }
    public Guid RecipeId { get; set; }
}