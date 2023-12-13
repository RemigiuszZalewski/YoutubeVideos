namespace MockVideo.Domain.Models;

public class ResetPasswordToken
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string TokenHash { get; set; } = string.Empty;
    public DateTime? ExpirationTime { get; set; }
}