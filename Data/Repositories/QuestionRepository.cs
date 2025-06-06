using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Entities;
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
            List<Questions> questions = new List<Questions>();

            questions = _dbContext.Questions.Include(g => g.QuestionGroupRelations).ThenInclude(gr => gr.Groups).ToList();

            return questions;
        }

        public List<Questions> GetByType(string questionType)
        {
            List<Questions> questions = new List<Questions>();

            questions = _dbContext.Questions.Include(g => g.QuestionGroupRelations).ThenInclude(gr => gr.Groups)
                .Where(t => t.QuestionType.CdQuestionType == questionType).ToList();

            return questions;
        }
        public List<Questions> GetByGroup(int idGroup)
        {
            List<Questions> questions = new List<Questions>();

            questions = _dbContext.Questions.Include(g => g.QuestionGroupRelations)
                .ThenInclude(gr => gr.Groups)
                .Where(a => a.QuestionGroupRelations.Any(b => b.Groups.IdGroups == idGroup))
                .ToList();

            return questions;
        }
        public List<Questions> GetByArea(string area)
        {
            List<Questions> questions = new List<Questions>();

            questions = _dbContext.Questions.Include(g => g.QuestionGroupRelations).ThenInclude(gr => gr.Groups)
                .Where( a=> a.CdArea == area).ToList();

            return questions;
        }

        public Questions GetByIdIncludes(int id)
        {
            return _dbContext.Questions.Include(g => g.QuestionGroupRelations)
                .ThenInclude(gr => gr.Groups)
                .Where(q => q.IdQuestions == id).FirstOrDefault();
        }
    }
}
