using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class UsersProfilesRepository: IUsersProfilesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersProfilesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<string>> GetProfilesByUserID(int userId)
        {
            var userProfiles = await _dbContext.UsersProfiles
               .Where(up => up.IdUser == userId)
               .Select(up => up.CdProfile)
               .ToListAsync();

            return userProfiles;
        }
    }
}
