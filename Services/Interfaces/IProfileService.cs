using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ResponseTransaction> AssignProfile(UserProfileDto profileDto);
        Task<ResponseTransaction> RemoveUserProfileAsync(UserProfileDto profileDto);
        Task<List<UserProfileDto>> GetProfilesByUserID(int userId);
    }
}
