using Email.API.Contracts;

namespace Email.API.Abstracts;

public interface IMailService
{
    Task SendEmailAsync(SendEmailRequest sendEmailRequest);
}