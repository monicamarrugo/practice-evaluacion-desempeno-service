using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class QuestionTypeService : IQuestionTypeService
    {
        private readonly IGenericRepository<QuestionType> _repository;

        public QuestionTypeService(IGenericRepository<QuestionType> repository)
        {
            _repository = repository;
        }
        public List<QuestionType> GetAllTipoPregunta()
        {
            var entities = _repository.GetAll();
            
            return entities.ToList();
        }
    }
}
