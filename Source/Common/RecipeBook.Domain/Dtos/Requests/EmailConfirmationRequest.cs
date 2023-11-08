namespace RecipeBook.Domain.Dtos.Requests;

public class EmailConfirmationRequest
{
    public string? Email { get; set; }
    public string? Code { get; set; }
}