using BusinessManager.Application.Interfaces.Settings;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private readonly SendGridClient _client;
    private readonly EmailAddress _fromAddress;

    public EmailService(IConfiguration configuration)
    {
        var apiKey = configuration["SendGrid:ApiKey"];
        _client = new SendGridClient(apiKey);
        _fromAddress = new EmailAddress(configuration["SendGrid:FromEmail"], configuration["SendGrid:FromName"]);
    }

    public async Task SendEmailAsync(string toEmail, string toName, string subject, string plainTextContent, string htmlContent)
    {
        var toAddress = new EmailAddress(toEmail, toName);
        var msg = MailHelper.CreateSingleEmail(_fromAddress, toAddress, subject, plainTextContent, htmlContent);
        var response = await _client.SendEmailAsync(msg);
    }
    public async Task SendLoginEmailAsync(string email, string firstName, string lastName, string password)
    {
        string emailSubject = "BusinessManager login information";
        string plainTextContent = $"Hello {firstName} {lastName},\n\nHere are your login details for the BusinessManager system:\nLogin: {email}\nPassword: {password}\n\nPlease change your password immediately after logging in for security purposes.";
        string htmlContent = $"<strong>Hello {firstName} {lastName},</strong><br/><br/>Here are your login details for the BusinessManager system:<br/>Login: {email}<br/>Password: {password}<br/><br/><strong>Please change your password immediately after logging in for security purposes.</strong>";

        await SendEmailAsync(email, $"{firstName} {lastName}", emailSubject, plainTextContent, htmlContent);
    }
}

