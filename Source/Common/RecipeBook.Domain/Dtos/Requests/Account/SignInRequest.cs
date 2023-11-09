namespace RecipeBook.Domain.Dtos.Requests.Account;

public class SignInRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}