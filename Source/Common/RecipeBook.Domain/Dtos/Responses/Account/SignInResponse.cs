namespace RecipeBook.Domain.Dtos.Responses.Account;

public class SignInResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiryDate { get; set; }
}