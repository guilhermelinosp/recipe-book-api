namespace RecipeBook.Domain.Dtos.Requests.Account;

public class ResetPasswordRequest
{
    public string Password { get; set; }
    public string Code { get; set; }
}