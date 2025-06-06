using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEvaluationService
    {
        List<EvaluationsDto> GetAllEvaluations();
        EvaluationsDto GetEvaluationsById(int idEvaluations);

        EvaluationCreateDto GetEvaluationsByIdInclude(int idEvaluations);
        Task<ActiveEvaluationsDto> GetActiveEvaluation(SearchActiveEvaluationDto dataSearch);

        ResponseTransaction SaveEvaluation(EvaluationCreateDto evaluationData);

        ResponseTransaction UpdateEvaluation(EvaluationCreateDto evaluationData);

        ResponseTransaction EnableEvaluation(EvaluationCreateDto evaluationData);
    }
}
