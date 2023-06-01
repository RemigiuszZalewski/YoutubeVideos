namespace ExceptionHandler.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(string message, Exception ex) : base(message, ex)
    {
        
    }
}