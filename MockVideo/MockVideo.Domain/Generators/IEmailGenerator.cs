namespace MockVideo.Domain.Generators;

public interface IEmailGenerator
{
    string GenerateResetPasswordEmailMessage(string fullName, string token);
}