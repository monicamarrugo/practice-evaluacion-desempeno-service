using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IQuestionariesConfigRepository
    {
        List<QuestionariesConfig> GetByQuestionaryIncludes(int idQuestionary);
        List<QuestionariesConfig> GetByQuestionaryIncludesComplete(int idQuestionary);
        QuestionariesConfig GetByIdIncludes(int id);
        List<QuestionariesConfig> GetByIdQuestionary(int idQuestionary);
        void DeleteConfig(int idQuestionary);

        void UpdateConfigs(List<QuestionariesConfigDto> UpdatedConfigs, int idQuestionary);
    }
}
