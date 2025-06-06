using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionTypeService
    {
        public List<QuestionType> GetAllTipoPregunta();
    }
}
