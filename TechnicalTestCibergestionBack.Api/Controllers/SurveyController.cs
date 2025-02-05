using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechnicalTestCibergestionBack.Application.DTOs;
using TechnicalTestCibergestionBack.Application.Services;

namespace TechnicalTestCibergestionBack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyService surveyService, IMapper mapper)
        {
            _surveyService = surveyService;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Voter,Admin")]
        public async Task<IActionResult> SubmitResponse([FromBody] SurveyResponseDto dto)
        {
            try
            {
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim == null)
                    return Unauthorized();

                int userId = int.Parse(userIdClaim.Value);

                var success = await _surveyService.SubmitSurveyResponseAsync(userId, dto);
                if (!success)
                    return BadRequest("Ya realizaste tu clasificación. Solo es posible votar una vez.");

                return Ok("Respuesta registrada con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("Nps")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetNps()
        {
            try
            {
                var result = await _surveyService.CalculateNpsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
