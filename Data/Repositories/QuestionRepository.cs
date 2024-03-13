using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class QuestionRepository: IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Questions> GetAllIncludes()
        {
            return _dbContext.Questions.Include(g => g.QuestionGroupRelations).ThenInclude(gr => gr.Groups).ToList();
        }

        public Questions GetByIdIncludes(int id)
        {
            return _dbContext.Questions.Include(g => g.QuestionGroupRelations)
                .ThenInclude(gr => gr.Groups)
                .Where(q => q.IdQuestions == id).FirstOrDefault();
        }
    }
}
