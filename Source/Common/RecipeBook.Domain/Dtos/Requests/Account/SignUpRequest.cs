namespace RecipeBook.Domain.Dtos.Requests.Account;

public class SignUpRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}