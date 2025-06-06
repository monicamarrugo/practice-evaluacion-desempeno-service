using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IUsersProfilesRepository
    {
        Task<List<string>> GetProfilesByUserID(int userId);
        Task<ApplicationUser> GetEmployee(ApplicationUser user);

        Task<ApplicationUser> GetUser(string username);
        Task<Boolean> RemoveProfile(UserProfileDto profileDto);
        Task<List<UsersProfiles>> GetProfilesEntityByUserID(int userId);
    }
}
