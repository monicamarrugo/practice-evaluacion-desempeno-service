using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Controllers
{
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost("assignProfile")]
        public async Task<IActionResult> AssignProfile(UserProfileDto profileDto)
        {
            var responseCreate = _profileService.AssignProfile(profileDto);
            return Ok(responseCreate);
        }

    }
}
