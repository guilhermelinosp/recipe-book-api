using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Domain.Entities;

[Table("TB_Connection")]
public class Connection
{
    public Connection(Guid accountId, Guid subAccountId)
    {
        SubAccountId = subAccountId;
        AccountId = accountId;
    }

    [Key] public Guid ConnectionId { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid SubAccountId { get; set; }
    public Guid AccountId { get; set; }
}