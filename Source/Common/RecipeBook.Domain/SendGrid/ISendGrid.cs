namespace RecipeBook.Domain.SendGrid;

public interface ISendGrid
{
    Task SendConfirmationEmailAsync(string email, string nome, string code);

    Task SendForgotPasswordEmailAsync(string email, string nome, string code);
}