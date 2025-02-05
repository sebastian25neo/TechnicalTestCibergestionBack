using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly string _connectionString;

    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM Users WHERE Username = @Username";
        return await connection.QueryFirstOrDefaultAsync<User>(query, new { Username = username });
    }

    public async Task AddUserAsync(User user)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO Users (Username, PasswordHash, Role, IsLocked, FailedAttempts) VALUES (@Username, @PasswordHash, @Role, @IsLocked, @FailedAttempts)";
        await connection.ExecuteAsync(query, user);
    }

    public async Task UpdateUserAsync(User user)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = @"
            UPDATE Users 
            SET PasswordHash = @PasswordHash, 
                Role = @Role, 
                IsLocked = @IsLocked, 
                FailedAttempts = @FailedAttempts
            WHERE Id = @Id";

        await connection.ExecuteAsync(query, user);
    }
}
