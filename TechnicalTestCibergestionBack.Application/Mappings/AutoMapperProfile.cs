using AutoMapper;
using TechnicalTestCibergestionBack.Application.DTOs;
using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {

        CreateMap<User, UserLoginDto>().ReverseMap();

        CreateMap<SurveyResponse, SurveyResponseDto>().ReverseMap();
    }
}
