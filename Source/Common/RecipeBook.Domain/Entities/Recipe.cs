using RecipeBook.Domain.Enum;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======

namespace RecipeBook.Domain.Entities;
>>>>>>> 5e0ad8c7692647508c252071f6c986e6f55a96d0

namespace RecipeBook.Domain.Entities;

[Table("TB_Recipe")]
public class Recipe
{
<<<<<<< HEAD
    [Key] public Guid RecipeId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string Title { get; set; }
    public Category Category { get; set; }
    public string PreparationMode { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
    public Guid AccountId { get; set; }
=======
    public Guid RecipeId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? Title { get; set; }
    public Category Category { get; set; }
    public string? MethodPreparation { get; set; }
    public ICollection<Ingredient>? Ingredients { get; set; }
>>>>>>> 5e0ad8c7692647508c252071f6c986e6f55a96d0
}