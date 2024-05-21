using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEvaluationService
    {
        public List<EvaluationsDto> GetAllEvaluations();
        public EvaluationsDto GetEvaluationsById(int idEvaluations);

        public EvaluationCreateDto GetEvaluationsByIdInclude(int idEvaluations);
        public List<EvaluationsDto> GetActiveEvaluation();

        ResponseTransaction SaveEvaluation(EvaluationCreateDto evaluationData);

        ResponseTransaction UpdateEvaluation(EvaluationCreateDto evaluationData);
    }
}
