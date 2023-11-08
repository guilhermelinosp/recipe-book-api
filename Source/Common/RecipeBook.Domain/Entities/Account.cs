using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities;

[Table("TB_Account")]
public class Account
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    [Required] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Required] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    [Required] public string? Name { get; set; }
    [Required] public string? Email { get; set; }
    [Required] public bool EmailConfirmed { get; set; } = false;
    [Required] public string? Code { get; set; } = string.Empty!;
    [Required] public string? Password { get; set; }
    [Required] public string? Phone { get; set; }
}