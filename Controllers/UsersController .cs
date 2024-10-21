using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILanguageService _languageService;

        public UsersController(IUserService userService, ILanguageService languageService)
        {
            _userService = userService; 
            _languageService = languageService;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto registerDto)
        {
            var responseCreate = _userService.Register(registerDto);
            return Ok(responseCreate);
        }   

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var usuario = _userService.GetUserById(id);

            return Ok(usuario);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {

            var responseCreate = _userService.UpdateUser(id, userDto);
            return Ok(responseCreate);
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var responseCreate = _userService.ChangePassword(changePasswordDto);
            return Ok(responseCreate);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var responseLogin = _userService.Login(loginDto);
            return Ok(responseLogin);
        }

        [HttpGet("listLanguages")]
        public IActionResult GetListLanguages()
        {
            var languages = this._languageService.GetAllLanguages();
            return Ok(languages);
        }
    }
}
