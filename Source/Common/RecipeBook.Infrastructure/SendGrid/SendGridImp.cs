using Microsoft.Extensions.Configuration;
using RecipeBook.Domain.SendGrid;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RecipeBook.Infrastructure.SendGrid;

public class SendGridImp : ISendGrid
{
    private readonly IConfiguration _configuration;

    public SendGridImp(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendConfirmationEmailAsync(string email, string nome, string code)
    {
        var sendGridClient = new SendGridClient(Environment.GetEnvironmentVariable("SendGrid:ApiKey")!);
        var from = new EmailAddress(Environment.GetEnvironmentVariable("SendGrid:FromEmail")!);
        var to = new EmailAddress(email);
        const string subject = "Confirmation Email Test API";
        const string plainTextContent = "Test API";
        var htmlContent = $"""
                           <p>Dear {nome},</p>

                           <p>We are excited to confirm your registration with the Test API. Your security code is:</p>

                           <p><strong>{code}</strong></p>

                           <p>This code is essential for accessing our services and ensuring the security of your account.</p>

                           <p>If you have any questions or encounter any issues, please don't hesitate to contact our support team at {_configuration["SendGrid:FromEmail"]}.</p>

                           <p>Thank you for choosing Test API. We look forward to serving you!</p>

                           <p>Best regards,</p>
                           <p>The Team at Test API</p>
                           """;

        await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
            htmlContent));
    }

    public async Task SendForgotPasswordEmailAsync(string email, string nome, string code)
    {
        var sendGridClient = new SendGridClient(_configuration["SendGrid:ApiKey"]!);
        var from = new EmailAddress(_configuration["SendGrid:FromEmail"]!);
        var to = new EmailAddress(email);
        const string subject = "Forgot Password Test API";
        const string plainTextContent = "Test API";

        var htmlContent = $"""
                           <p>Dear {nome},</p>

                           <p>We have received a request to reset your password for your Test API account.</p>

                           <p>If you did not initiate this request, please disregard this email. If you did, please use the following security code to reset your password:</p>

                           <p><strong>{code}</strong></p>

                           <p>If you have any questions or encounter any issues, please don't hesitate to contact our support team at {_configuration["SendGrid:FromEmail"]}.</p>

                           <p>Thank you for choosing Test API. We look forward to assisting you with your password reset!</p>

                           <p>Best regards,</p>
                           <p>The Team at Test API</p>
                           """;


        await sendGridClient.SendEmailAsync(MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,
            htmlContent));
    }
}