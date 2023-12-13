namespace MockVideo.Domain.Primitives;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTimeOffset TokenExpirationDate { get; set; }
}