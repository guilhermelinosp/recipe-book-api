using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Dtos.Requests;

public class SignInRequest
{
    public SignInRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}