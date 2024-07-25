using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionariesConfigService
    {
        public List<QuestionariesConfigDto> GetByQuestionary(int idQuestionary);
        CreateQuestionaryConfigDto GetCompleteByQuestionary(int idQuestionary);
        ResponseTransaction SaveQuestionariesConfig(List<QuestionariesConfigDto> config);
        ResponseTransaction UpdateQuestionariesConfig(List<QuestionariesConfigDto> config, int idQuestionary);

        List<RecordDetailsDto> GetQuestionaryToRecord(int idQuestionary);
    }
}
