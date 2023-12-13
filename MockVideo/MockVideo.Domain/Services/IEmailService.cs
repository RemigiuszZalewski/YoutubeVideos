namespace MockVideo.Domain.Services;

public interface IEmailService
{
    Task SendEmailAsync(string emailAddress, string emailMessage);
}