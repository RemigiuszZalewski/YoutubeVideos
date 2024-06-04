namespace Email.API.Contracts;

public record SendEmailRequest(string Recipient, string Subject, string Body);