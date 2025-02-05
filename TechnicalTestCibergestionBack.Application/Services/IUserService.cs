using TechnicalTestCibergestionBack.Application.DTOs;

namespace TechnicalTestCibergestionBack.Application.Services;

public interface IUserService
{
    Task<string?> AuthenticateAsync(UserLoginDto loginDto);
    Task<bool> RegisterUserAsync(UserRegisterDto newUser);
}

