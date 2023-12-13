namespace MockVideo.Domain.Exceptions;

public class RefreshTokenException : Exception
{
    public RefreshTokenException(string message) : base(message)
    {
    }
}