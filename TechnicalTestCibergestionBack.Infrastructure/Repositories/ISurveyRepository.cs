using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Infrastructure.Repositories
{
    public interface ISurveyRepository
    {
        Task<bool> HasUserVotedAsync(int userId);
        Task AddSurveyResponseAsync(SurveyResponse response);
        Task<List<SurveyResponse>> GetAllResponsesAsync();
    }
}
