using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Domain.Dtos.Requests;

public class SignUpRequest
{
    public SignUpRequest(string name, string email, string password, string phone)
    {
        Name = name;
        Email = email;
        Password = password;
        Phone = phone;
    }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Phone { get; set; }
}