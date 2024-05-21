using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionaryTypeService
    {
        public List<QuestionaryTypes> GetAllQuestionaryTypes();
    }
}
