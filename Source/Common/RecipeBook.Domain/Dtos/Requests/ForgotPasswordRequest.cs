using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Dtos.Requests;

public class ForgotPasswordRequest
{
    public ForgotPasswordRequest(string email)
    {
        Email = email;
    }

    [Required]
    public string Email { get; set; }
}