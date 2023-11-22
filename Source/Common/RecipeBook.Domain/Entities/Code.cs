using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Entities;

public class Code
{
    [Key] public Guid CodeId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string CodeValue { get; set; } = string.Empty!;
    public Guid AccountId { get; set; }
}