namespace MockVideo.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HashedPassword { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTimeOffset RefreshTokenExpirationDate { get; set; } = DateTimeOffset.MinValue;

    public override string ToString()
    {
        return FirstName + " " + LastName;
    }
}