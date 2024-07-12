using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEvaluationRepository
    {
        List<Evaluations> GetActiveEvaluations(SearchActiveEvaluationDto dataSearch);
        public Evaluations GetEvaluationsByIdInclude(int idEvaluations);

        void UpdateEvaluation(EvaluationCreateDto evaluationData);
        void EnableEvaluation(EvaluationCreateDto evaluationData);

        IQueryable<Evaluations> GetEvaluationsInclude();
    }
}
