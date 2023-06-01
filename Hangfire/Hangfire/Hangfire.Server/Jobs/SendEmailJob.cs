using Hangfire.Server.Options;
using Hangfire.Shared.Jobs;
using Hangfire.Shared.Services;
using Microsoft.Extensions.Options;

namespace Hangfire.Server.Jobs;

public class SendEmailJob : ISendEmailJob
{
    private readonly IEmailService _emailService;
    private readonly string _email;
    private readonly string _password;
    
    public SendEmailJob(IEmailService emailService, IOptions<ServerOptions> serverOptions)
    {
        _emailService = emailService;
        _email = serverOptions.Value.Email;
        _password = serverOptions.Value.Password;
    }
    
    public async Task Execute()
    {
        await _emailService.SendEmailAsync(_email, _password);
    }
}