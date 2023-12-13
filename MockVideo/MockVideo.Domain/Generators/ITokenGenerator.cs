using MockVideo.Domain.Models;
using RefreshToken = MockVideo.Domain.Primitives.RefreshToken;

namespace MockVideo.Domain.Generators;

public interface ITokenGenerator
{
    string GenerateJwt(User user);
    RefreshToken GenerateRefreshToken();
}