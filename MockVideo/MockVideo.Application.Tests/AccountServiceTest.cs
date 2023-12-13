using MockVideo.Application.Services;
using MockVideo.Domain.Dtos;
using MockVideo.Domain.Exceptions;
using MockVideo.Domain.Generators;
using MockVideo.Domain.Models;
using MockVideo.Domain.Primitives;
using MockVideo.Domain.Repositories;
using MockVideo.Domain.Requests;
using MockVideo.Domain.Services;
using MockVideo.Domain.Utils;
using Moq;

namespace MockVideo.Application.Tests;

public class AccountServiceTest
{
    private readonly Mock<IStringHasher> _stringHasherMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
    private readonly Mock<IEmailGenerator> _emailGeneratorMock;
    private readonly Mock<IEmailService> _emailServiceMock;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IResetPasswordTokenRepository> _resetPasswordTokenRepositoryMock;
    private readonly AccountService _accountService;
    private readonly LoginRequest _loginRequest;
    private readonly User _user;
    
    public AccountServiceTest()
    {
        _loginRequest = new LoginRequest
        {
            Email = "abc@email.com",
            Password = "abcabc"
        };
        
        _user = new User
        {
            Id = 1,
            Email = _loginRequest.Email,
            FirstName = "firstName",
            LastName = "lastName",
            HashedPassword = "hash",
            RefreshToken = "refreshToken",
            RefreshTokenExpirationDate = DateTimeOffset.MinValue
        };
        
        _stringHasherMock = new Mock<IStringHasher>();
        _tokenGeneratorMock = new Mock<ITokenGenerator>();
        _emailGeneratorMock = new Mock<IEmailGenerator>();
        _emailServiceMock = new Mock<IEmailService>();
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _resetPasswordTokenRepositoryMock = new Mock<IResetPasswordTokenRepository>();
        
        _accountRepositoryMock.Setup(x => x.GetByEmailAsync(_loginRequest.Email))
            .ReturnsAsync(_user);
        
        _accountService = new AccountService(_stringHasherMock.Object, _tokenGeneratorMock.Object,
            _emailGeneratorMock.Object,
            _emailServiceMock.Object, _accountRepositoryMock.Object,
            _resetPasswordTokenRepositoryMock.Object);
    }

    [Fact]
    public async Task LoginAsync_PasswordIsInvalid_LoginFailedExceptionIsThrown()
    {
        //Arrange
        _stringHasherMock.Setup(x =>
            x.HashesMatch(_user.HashedPassword, _loginRequest.Password)).Returns(false);

        //Act
        var act = () => _accountService.LoginAsync(_loginRequest);

        //Assert
        await Assert.ThrowsAsync<LoginFailedException>(act);
        _accountRepositoryMock.Verify(x => x.GetByEmailAsync(_loginRequest.Email),
            Times.Once);
        _stringHasherMock.Verify(x => x.HashesMatch(_user.HashedPassword, _loginRequest.Password),
            Times.Once);
        _tokenGeneratorMock.Verify(x => x.GenerateRefreshToken(), Times.Never);
    }

    [Fact]
    public async Task LoginAsync_AllDataIsCorrect_UserDtoReturned()
    {
        //Arrange
        _stringHasherMock.Setup(x =>
            x.HashesMatch(_user.HashedPassword, _loginRequest.Password)).Returns(true);

        _accountRepositoryMock.Setup(x =>
            x.UpdateAsync(_user)).Returns(Task.CompletedTask);
        
        var refreshToken = new RefreshToken
        {
            Token = _user.RefreshToken,
            TokenExpirationDate = _user.RefreshTokenExpirationDate
        };

        var expectedUserDto = new UserDto(_user.FirstName + " " + _user.LastName, _user.Id,
            "tokenJwt", refreshToken.Token);

        _tokenGeneratorMock.Setup(x => x.GenerateJwt(_user)).Returns("tokenJwt");
        _tokenGeneratorMock.Setup(x => x.GenerateRefreshToken()).Returns(refreshToken);

        //Act
        var userDto = await _accountService.LoginAsync(_loginRequest);
        
        //Assert
        Assert.Equal(userDto, expectedUserDto);
        _accountRepositoryMock.Verify(x => x.GetByEmailAsync(_loginRequest.Email),
            Times.Once);
        _stringHasherMock.Verify(x => x.HashesMatch(_user.HashedPassword, _loginRequest.Password),
            Times.Once);
        _tokenGeneratorMock.Verify(x => x.GenerateRefreshToken(), Times.Once);
        _tokenGeneratorMock.Verify(x => x.GenerateJwt(_user), Times.Once);
        _accountRepositoryMock.Verify(x => x.UpdateAsync(_user), Times.Once);
    }
}