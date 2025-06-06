using AutoMapper;
using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Enums;
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
        public async Task<List<Evaluations>> GetActiveEvaluations(SearchActiveEvaluationDto dataSearch)
        {
            return await _dbContext.Evaluations
                .Include(e => e.EvaluationsPositions)
                .Include(s => s.Escales).ThenInclude(v => v.EscalesValues)
                .Where(e =>
                            (e.IndEnabled == true)
                            &&
                            ((e.StartDate == null) || (dataSearch.currentDate >= e.StartDate)
                                                        && (dataSearch.currentDate <= e.EndDate))
                            &&
                            ((e.IDProcessLeader == null) || (e.IDProcessLeader == dataSearch.idProcessLeader))
                            &&
                            ((e.CdDivisions == null) || (e.CdDivisions == dataSearch.cdDivisions))
                        )
                .ToListAsync();
        }

        public IQueryable<Evaluations> GetEvaluationsInclude()
        {
            return _dbContext.Evaluations
                .Include(e => e.Questionaries)
                .Include(l => l.Employees)
                .AsQueryable();
        }

        public Evaluations GetEvaluationsByIdInclude(int idEvaluations)
        {
            return _dbContext.Evaluations
                .Include(e => e.EvaluationsPositions)
                .Include(l => l.Employees)
                .Where(ev => ev.IdEvaluations == idEvaluations)
                .FirstOrDefault();
        }
        public void EnableEvaluation(EvaluationCreateDto evaluationData)
        {
            var evaluation = _mapper.Map<Evaluations>(evaluationData.evaluation);
            evaluation.ModifiedDate = DateTime.Now;
            _dbContext.Evaluations.Update(evaluation);
            _dbContext.SaveChanges();
        }

        public void UpdateEvaluation(EvaluationCreateDto evaluationData)
        {
            var evaluation = _mapper.Map<Evaluations>(evaluationData.evaluation);
            evaluation.ModifiedDate = DateTime.Now;
            _dbContext.Evaluations.Update(evaluation);

            if (evaluationData.evaluation.cdTypeEvaluation != QuestionaryTypeEnum.Indicators.GetStringValue())
            {
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
            }

            _dbContext.SaveChanges();
        }
    }
}
