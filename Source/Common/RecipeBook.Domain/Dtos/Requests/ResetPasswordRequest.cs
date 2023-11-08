namespace RecipeBook.Domain.Dtos.Requests;

public class ResetPasswordRequest
{
    public string? Password { get; set; }
    public string? Code { get; set; }
}