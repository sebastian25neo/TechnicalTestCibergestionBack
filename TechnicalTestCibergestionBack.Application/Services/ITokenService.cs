﻿namespace TechnicalTestCibergestionBack.Application.Services;

public interface ITokenService
{
    string GenerateToken(string username, string role, int userId);
}