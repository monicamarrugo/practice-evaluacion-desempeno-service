using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEvaluationRepository
    {
        List<Evaluations> GetActiveEvaluations(DateTime current);
        public Evaluations GetEvaluationsByIdInclude(int idEvaluations);

        void UpdateEvaluation(EvaluationCreateDto evaluationData);

        IQueryable<Evaluations> GetEvaluationsInclude();
    }
}
