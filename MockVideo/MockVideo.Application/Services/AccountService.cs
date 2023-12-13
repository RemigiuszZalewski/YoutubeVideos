using MockVideo.Domain.Dtos;
using MockVideo.Domain.Exceptions;
using MockVideo.Domain.Generators;
using MockVideo.Domain.Models;
using MockVideo.Domain.Primitives;
using MockVideo.Domain.Repositories;
using MockVideo.Domain.Requests;
using MockVideo.Domain.Services;
using MockVideo.Domain.Utils;

namespace MockVideo.Application.Services;

public class AccountService : IAccountService
{
    private readonly IStringHasher _stringHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IEmailGenerator _emailGenerator;
    private readonly IEmailService _emailService;
    private readonly IAccountRepository _accountRepository;
    private readonly IResetPasswordTokenRepository _resetPasswordTokenRepository;
    private const string LoginFailedExceptionMessage = "Invalid username or password";

    public AccountService(IStringHasher stringHasher, ITokenGenerator tokenGenerator,
        IEmailGenerator emailGenerator, IEmailService emailService, IAccountRepository accountRepository,
        IResetPasswordTokenRepository resetPasswordTokenRepository)
    {
        _stringHasher = stringHasher;
        _tokenGenerator = tokenGenerator;
        _emailGenerator = emailGenerator;
        _emailService = emailService;
        _accountRepository = accountRepository;
        _resetPasswordTokenRepository = resetPasswordTokenRepository;
    }
    
    public async Task<UserDto> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _accountRepository.GetByEmailAsync(loginRequest.Email);
        var passwordIsValid = _stringHasher.HashesMatch(user.HashedPassword, loginRequest.Password);

        if (!passwordIsValid)
        {
            throw new LoginFailedException(LoginFailedExceptionMessage);
        }

        var jwt = _tokenGenerator.GenerateJwt(user);
        var refreshToken = _tokenGenerator.GenerateRefreshToken();

        await AssignRefreshToken(refreshToken, user);

        return new UserDto(user.FirstName + " " + user.LastName, user.Id, jwt, refreshToken.Token);
    }

    private async Task AssignRefreshToken(RefreshToken refreshToken, User user)
    {
        user.RefreshToken = refreshToken.Token;
        user.RefreshTokenExpirationDate = refreshToken.TokenExpirationDate;

        await _accountRepository.UpdateAsync(user);
    }
}