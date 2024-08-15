using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Files> GetByIdRecordAsync(int idRecord)
        {
            return _dbContext.Files
                .Where(f => f.IdEvaluationRecord == idRecord).FirstOrDefaultAsync();
        }
    }
}
