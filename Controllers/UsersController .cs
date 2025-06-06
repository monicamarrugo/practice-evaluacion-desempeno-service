using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
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
        

        // GET: api/Users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var usuario = await _userService.GetUserById(id);

            return Ok(usuario);
        }

        // PUT: api/Users/{id}
        [HttpPost("updateUser")]
        public async Task<IActionResult> UpdateUser(UserDto userDto)
        {

            var responseCreate = await  _userService.UpdateUser(userDto);
            return Ok(responseCreate);
        }
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var responseCreate = await _userService.ChangePassword(changePasswordDto);
            return Ok(responseCreate);
        }

        

        [HttpGet("listLanguages")]
        public IActionResult GetListLanguages()
        {
            var languages = this._languageService.GetAllLanguages();
            return Ok(languages);
        }

        [Authorize]
        [HttpGet("userByEmployee/{idEmployee}")]
        public async Task<IActionResult> GetUserByEmployeeId(int idEmployee)
        {
            var usuario = await _userService.GetUserByEmployeeId(idEmployee);

            return Ok(usuario);
        }
    }
}
