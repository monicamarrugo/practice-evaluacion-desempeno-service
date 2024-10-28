using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
        public async Task<ApplicationUser> GetUser(string username)
        {
            var user = await _dbContext.Users
            .Where(u => u.UserName == username)
            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<ApplicationUser> GetEmployee(ApplicationUser user)
        {
           await _dbContext.Entry(user).Reference(u => u.Employees).LoadAsync();
            return user;
        }
    }
}
