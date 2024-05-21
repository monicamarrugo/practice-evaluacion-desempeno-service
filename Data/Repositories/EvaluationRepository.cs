using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EvaluationRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public List<Evaluations> GetActiveEvaluations(DateTime current)
        {
            return _dbContext.Evaluations
                .Where(e => current >= e.StartDate && current <= e.EndDate && e.IndEnabled.GetValueOrDefault()).ToList();
        }

        public IQueryable<Evaluations> GetEvaluationsInclude()
        {
            return _dbContext.Evaluations
                .Include(e => e.Questionaries)
                .AsQueryable();
        }

        public Evaluations GetEvaluationsByIdInclude(int idEvaluations)
        {
            return _dbContext.Evaluations
                .Include(e => e.EvaluationsPositions)
                .Where(ev => ev.IdEvaluations == idEvaluations)
                .FirstOrDefault();
        }

        public void UpdateEvaluation(EvaluationCreateDto evaluationData)
        {
            var evaluation = _mapper.Map<Evaluations>(evaluationData.evaluation);
            evaluation.ModifiedDate = DateTime.Now;
            _dbContext.Evaluations.Update(evaluation);

            
            // Recuperar los cargos relacionados a la evaluacion 
            var currentPositions = _dbContext.EvaluationsPositions
                .Where(p => p.IdEvaluations == evaluation.IdEvaluations).ToList();

            // Actualizar las posiciones existentes y agregar las nuevas
            foreach (var uPosition in evaluationData.evaluationPosition)
            {
                var existingPosition = currentPositions.FirstOrDefault(q => q.IdPosition == uPosition.idPosition);
                var positiion = _mapper.Map<EvaluationsPositions>(uPosition);
                if (existingPosition == null)
                {
                    // Agregar la nueva posicion
                    _dbContext.EvaluationsPositions.Add(positiion);
                }
            }

            // Eliminar las posiciones existentes que no están en la lista nueva
            foreach (var existingPosition in currentPositions)
            {
                if (!evaluationData.evaluationPosition.Any(q => q.idPosition == existingPosition.IdPosition))
                {
                    _dbContext.EvaluationsPositions.Remove(existingPosition);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
