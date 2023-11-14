using RecipeBook.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities;

[Table("TB_Recipe")]
public class Recipe
{
    [Key] public Guid RecipeId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Title { get; set; }
    public Category Category { get; set; }
    public string PreparationMode { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    public Guid AccountId { get; set; }
}