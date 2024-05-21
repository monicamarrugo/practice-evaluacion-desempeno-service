using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class QuestionaryTypeService : IQuestionaryTypeService
    {
        private readonly IGenericRepository<QuestionaryTypes> _repository;

        public QuestionaryTypeService(IGenericRepository<QuestionaryTypes> repository)
        {
            _repository = repository;
        }
        public List<QuestionaryTypes> GetAllQuestionaryTypes()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }
    }
}
