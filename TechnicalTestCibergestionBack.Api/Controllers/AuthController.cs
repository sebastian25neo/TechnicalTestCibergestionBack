using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TechnicalTestCibergestionBack.Application.DTOs;
using TechnicalTestCibergestionBack.Application.Services;

namespace TechnicalTestCibergestionBack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            try
            {
                var token = await _userService.AuthenticateAsync(loginDto);
                if (token == null)
                    return Unauthorized("Usuario/contraseña incorrectos o cuenta bloqueada.");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto newUser)
        {
            try
            {
                var registered = await _userService.RegisterUserAsync(newUser);
                if (!registered)
                {
                    return BadRequest("El usuario ya existe.");
                }

                return Ok("Usuario registrado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
