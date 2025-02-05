using TechnicalTestCibergestionBack.Application.DTOs;

namespace TechnicalTestCibergestionBack.Application.Services;

public interface ISurveyService
{
    Task<bool> SubmitSurveyResponseAsync(int userId, SurveyResponseDto responseDto);
    Task<NpsResultDto> CalculateNpsAsync();
}
