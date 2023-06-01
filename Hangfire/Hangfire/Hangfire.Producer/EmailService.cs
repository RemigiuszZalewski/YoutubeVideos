using System.Net;
using System.Net.Mail;
using Hangfire.Shared.Services;

namespace Hangfire.Producer;

public class EmailService : IEmailService
{
    public async Task SendEmailAsync(string fromEmail, string password)
    {
        var email = new MailMessage();
        email.From = new MailAddress(fromEmail);
        email.Subject = "Hangfire Job";
        email.To.Add(new MailAddress("customerhangfiredemo@gmail.com"));
        email.Body = $"<html><body>Hello this is an email sent from HangfireJob</body></html>";
        email.IsBodyHtml = true;

        using var smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true
        };
        
        await smtpClient.SendMailAsync(email);
    }
}