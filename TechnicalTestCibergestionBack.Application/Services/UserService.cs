using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using TechnicalTestCibergestionBack.Application.DTOs;
using TechnicalTestCibergestionBack.Infrastructure.Repositories;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;  // <-- Inyectar AutoMapper

    public UserService(IUserRepository userRepository, ITokenService tokenService, IMapper mapper)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<string?> AuthenticateAsync(UserLoginDto loginDto)
    {
        var user = await _userRepository.GetByUsernameAsync(loginDto.Username);
        if (user == null || user.IsLocked)
            return null;

        bool passwordCorrect = VerifyPassword(loginDto.Password, user.PasswordHash);
        if (!passwordCorrect)
        {
            user.FailedAttempts += 1;
            if (user.FailedAttempts >= 3) user.IsLocked = true;
            await _userRepository.UpdateUserAsync(user);
            return null;
        }

        user.FailedAttempts = 0;
        await _userRepository.UpdateUserAsync(user);

        return _tokenService.GenerateToken(user.Username, user.Role, user.Id);
    }

    public async Task<bool> RegisterUserAsync(UserRegisterDto newUser)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(newUser.Username);
        if (existingUser != null) return false;

        var user = _mapper.Map<User>(newUser); // <-- Usamos AutoMapper
        user.PasswordHash = HashPassword(newUser.Password);
        user.Role = newUser.Role.ToLower() == "admin" ? "Admin" : "Voter";

        await _userRepository.AddUserAsync(user);
        return true;
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static bool VerifyPassword(string input, string hash)
    {
        return HashPassword(input) == hash;
    }
}
