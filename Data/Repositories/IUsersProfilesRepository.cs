using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IUsersProfilesRepository
    {
        Task<List<string>> GetProfilesByUserID(int userId);
        Task<ApplicationUser> GetEmployee(ApplicationUser user);

        Task<ApplicationUser> GetUser(string username);
    }
}
