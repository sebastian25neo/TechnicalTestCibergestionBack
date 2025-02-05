using AutoMapper;
using TechnicalTestCibergestionBack.Application.DTOs;
using TechnicalTestCibergestionBack.Infrastructure.Repositories;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Application.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly IMapper _mapper;  

    public SurveyService(ISurveyRepository surveyRepository, IMapper mapper)
    {
        _surveyRepository = surveyRepository;
        _mapper = mapper;
    }

    public async Task<bool> SubmitSurveyResponseAsync(int userId, SurveyResponseDto responseDto)
    {
        if (await _surveyRepository.HasUserVotedAsync(userId))
            return false;

        var response = _mapper.Map<SurveyResponse>(responseDto); 
        response.UserId = userId;
        await _surveyRepository.AddSurveyResponseAsync(response);
        return true;
    }

    public async Task<NpsResultDto> CalculateNpsAsync()
    {
        var responses = await _surveyRepository.GetAllResponsesAsync();
        if (responses.Count == 0)
            return new NpsResultDto { NPS = 0 };

        int promoters = responses.Count(r => r.Score >= 9);
        int neutrals = responses.Count(r => r.Score >= 7 && r.Score <= 8);
        int detractors = responses.Count(r => r.Score <= 6);

        double nps = ((double)(promoters - detractors) / responses.Count) * 100;

        return new NpsResultDto
        {
            Promoters = promoters,
            Neutrals = neutrals,
            Detractors = detractors,
            NPS = Math.Round(nps, 2)
        };
    }
}
