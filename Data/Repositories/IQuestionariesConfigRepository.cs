using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IQuestionariesConfigRepository
    {
        Task<List<QuestionariesConfig>> GetByQuestionaryIncludes(int idQuestionary);
        List<QuestionariesConfig> GetByQuestionaryIncludesComplete(int idQuestionary);
        QuestionariesConfig GetByIdIncludes(int id);
        List<QuestionariesConfig> GetByIdQuestionary(int idQuestionary);
        void DeleteConfig(int idQuestionary);

        void UpdateConfigs(List<QuestionariesConfigDto> UpdatedConfigs, int idQuestionary);
    }
}
