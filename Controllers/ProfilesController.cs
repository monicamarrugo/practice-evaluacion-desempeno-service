using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services;
using EvaluacionDesempenoApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var responseAssign = await _profileService.AssignProfile(profileDto);
            return Ok(responseAssign);
        }

        [HttpPost("RemoveUserProfile")]
        public async Task<IActionResult> RemoveUserProfile(UserProfileDto profileDto)
        {
            var responseRemove = await _profileService.RemoveUserProfileAsync(profileDto);
            return Ok(responseRemove);
        }

        [HttpPost("GetProfilesByUserID")]
        public async Task<IActionResult> GetProfilesByUserID(int userId)
        {
            var profiles = await _profileService.GetProfilesByUserID(userId);
            return Ok(profiles);
        }

    }
}
