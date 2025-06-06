using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class QuestionariesConfigRepository : IQuestionariesConfigRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionariesConfigRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public QuestionariesConfig GetByIdIncludes(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<QuestionariesConfig>> GetByQuestionaryIncludes(int idQuestionary)
        {
            List<QuestionariesConfig> listQuestion = null;
            if (idQuestionary > 0)
            {
                listQuestion = await _dbContext.QuestionariesConfig.Include(p => p.Questions)
                .ThenInclude(d => d.QuestionGroupRelations)
                .ThenInclude(s => s.Groups)
                .Where(q => q.IdQuestionary == idQuestionary).ToListAsync();
            }
            return listQuestion;
        }

        public List<QuestionariesConfig> GetByIdQuestionary(int idQuestionary)
        {
            return _dbContext.QuestionariesConfig
                .Where(q => q.IdQuestionary == idQuestionary).ToList();
        }

        public void DeleteConfig(int idQuestionary)
        {
            var entitiesRemove = _dbContext.QuestionariesConfig.Where(q => q.IdQuestionary == idQuestionary);
            _dbContext.QuestionariesConfig.RemoveRange(entitiesRemove);
            _dbContext.SaveChanges();
        }
        public List<QuestionariesConfig> GetByQuestionaryIncludesComplete(int idQuestionary)
        {
            return _dbContext.QuestionariesConfig
                .Include(c => c.Questionary)
                .Include(p => p.Questions)
                .ThenInclude(d => d.QuestionGroupRelations)
                .ThenInclude(s => s.Groups)
                .Where(q => q.IdQuestionary == idQuestionary).ToList();
        }

        public void UpdateConfigs(List<QuestionariesConfigDto> UpdatedConfigs, int idQuestionary)
        {
            // Recuperar el cuestionario existente con sus preguntas
            var currentConfigs = _dbContext.QuestionariesConfig
                .Where(q => q.IdQuestionary == idQuestionary).ToList();

            // Actualizar las preguntas existentes y agregar las nuevas
            foreach (var uConfig in UpdatedConfigs)
            {
                var existingQuestion = currentConfigs.FirstOrDefault(q => q.IdQuestions == uConfig.idQuestions);
                //var config = _mapper.Map<QuestionariesConfig>(uConfig);
                if (existingQuestion != null)
                {
                    // Actualizar la pregunta existente
                    // _dbContext.QuestionariesConfig.Update(config);
                    _mapper.Map(uConfig, existingQuestion);
                }
                else
                {
                    // Agregar la nueva pregunta
                    //_dbContext.QuestionariesConfig.Add(config);
                    var newConfig = _mapper.Map<QuestionariesConfig>(uConfig);
                    _dbContext.QuestionariesConfig.Add(newConfig);
                }
            }

            // Eliminar las preguntas existentes que no están en la lista nueva
            foreach (var existingConfig in currentConfigs)
            {
                if (!UpdatedConfigs.Any(q => q.idQuestions == existingConfig.IdQuestions))
                {
                    _dbContext.QuestionariesConfig.Remove(existingConfig);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
