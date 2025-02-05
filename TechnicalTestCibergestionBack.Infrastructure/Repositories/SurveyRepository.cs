using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Infrastructure.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly string _connectionString;

    public SurveyRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<bool> HasUserVotedAsync(int userId)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT COUNT(1) FROM SurveyResponses WHERE UserId = @UserId";
        int count = await connection.ExecuteScalarAsync<int>(query, new { UserId = userId });
        return count > 0;
    }

    public async Task AddSurveyResponseAsync(SurveyResponse response)
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "INSERT INTO SurveyResponses (UserId, Score, SubmittedAt) VALUES (@UserId, @Score, @SubmittedAt)";
        await connection.ExecuteAsync(query, response);
    }

    public async Task<List<SurveyResponse>> GetAllResponsesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        string query = "SELECT * FROM SurveyResponses";
        return (await connection.QueryAsync<SurveyResponse>(query)).ToList();
    }
}
