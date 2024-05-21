using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionaryService
    {
        public List<QuestionaryDto> GetAllQuestionaries();
        public List<QuestionaryDto> GetAllQuestionariesByType(string type);

        ResponseTransaction SaveQuestionary(CreateQuestionaryConfigDto questionaryConfig);
        ResponseTransaction UpdateQuestionary(CreateQuestionaryConfigDto questionaryConfig);
        public QuestionaryDto GetById(int id);
    }
}
