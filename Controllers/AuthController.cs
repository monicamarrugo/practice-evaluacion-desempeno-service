using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EvaluacionDesempenoApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Authorize]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            var responseCreate = await _authService.Register(registerDto);
            return Ok(responseCreate);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var responseLogin = await _authService.Login(loginDto);
            return Ok(responseLogin);
        }
    }
}
