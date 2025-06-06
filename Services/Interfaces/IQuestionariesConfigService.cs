using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IQuestionariesConfigService
    {
        Task<List<QuestionariesConfigDto>> GetByQuestionary(int idQuestionary);
        CreateQuestionaryConfigDto GetCompleteByQuestionary(int idQuestionary);
        ResponseTransaction SaveQuestionariesConfig(List<QuestionariesConfigDto> config);
        ResponseTransaction UpdateQuestionariesConfig(List<QuestionariesConfigDto> config, int idQuestionary);

        Task<List<RecordDetailsDto>> GetQuestionaryToRecord(int idQuestionary);
    }
}
