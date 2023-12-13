namespace MockVideo.Domain.Dtos;

public sealed record UserDto(string fullName, int userId, string accessToken, string refreshToken);