using MockVideo.Domain.Dtos;
using MockVideo.Domain.Requests;

namespace MockVideo.Domain.Services;

public interface IAccountService
{
    Task<UserDto> LoginAsync(LoginRequest loginRequest);
}