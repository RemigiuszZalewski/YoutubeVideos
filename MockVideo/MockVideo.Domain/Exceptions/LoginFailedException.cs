namespace MockVideo.Domain.Exceptions;

public class LoginFailedException : Exception
{
    public LoginFailedException(string message) : base(message)
    {
    }
}