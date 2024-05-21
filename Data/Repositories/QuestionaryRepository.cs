using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class QuestionaryRepository : IQuestionaryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionaryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Questionaries> GetAllIncludes()
        {
            return _dbContext.Questionaries
                .Include(q => q.QuestionaryTypes)
                .Include(q => q.Area)
                .OrderByDescending(q => q.IdQuestionary).ToList();
        }

        public List<Questionaries> GetByTypeIncludes(string type)
        {
            return _dbContext.Questionaries.Include(q => q.QuestionaryTypes).Include(q => q.Area)
                .Where(t => t.CdQuestionaryType.Equals(type)).ToList();
        }

        public Questionaries GetByIdIncludes(int id)
        {
            return _dbContext.Questionaries.Include(p => p.Employees)
                .Include(d => d.QuestionaryTypes)
                .Where(q => q.IdQuestionary == id).FirstOrDefault();
        }
    }
}
