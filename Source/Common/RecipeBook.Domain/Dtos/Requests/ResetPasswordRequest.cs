using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Dtos.Requests;

public class ResetPasswordRequest
{
    public ResetPasswordRequest(string password, string code)
    {
        Password = password;
        Code = code;
    }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Code { get; set; }
}