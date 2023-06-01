namespace Hangfire.Server.Options;

public class ServerOptions
{
    public const string ServerOptionsKey = "ServerOptions";
    
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}