using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Employees> GetAllIncludes()
        {
            return _dbContext.Employees.Include(p => p.Positions)
                .Include(d => d.Divisions).Include(s => s.Responsible).ToList();
        }
        public List<Employees> GetAllIncludesByResponsible(int idResponsible)
        {
            return _dbContext.Employees.Include(p => p.Positions)
                .Include(d => d.Divisions)
                .Where(e => e.IDResponsible == idResponsible && e.Enabled == true)
                .ToList();
        }

        public List<EmployeeRecordDto> GetEmployeesFromRecords(SearchEmployeesDto data)
        {
            var queryEmployees = from employee in _dbContext.Employees
                        .Include(e => e.Divisions)
                        .Include(e => e.Positions)
                        where employee.IDResponsible == data.idResponsible && employee.Enabled
                        join evaluationRecord in _dbContext.EvaluationRecord
                            .Include(er => er.EvaluationStates)
                        on employee.IdEmployees equals evaluationRecord.IdEmployee into evaluationGroup
                        from evaluationRecord in evaluationGroup.DefaultIfEmpty()
                        where evaluationRecord == null || evaluationRecord.IdEvaluations == data.idEvaluation
                        select new EmployeeRecordDto
                        {
                            idEmployee = employee.IdEmployees,
                            names = employee.Names,
                            lastNames = employee.LastNames,
                            identification = employee.Identification,
                            email =  employee.Email,
                            nameDivisions = employee.Divisions.Name,
                            namePosition = employee.Positions.NamePosition,
                            existsRecord = evaluationRecord == null? false: true,
                            idRecordEvaluation =  evaluationRecord == null? null:evaluationRecord.IdEvaluationRecord,
                            cdRecordState = evaluationRecord == null ? null : evaluationRecord.EvaluationStates.CdEvaluationStates,
                            recordStateES = evaluationRecord == null ? null : evaluationRecord.EvaluationStates.NameEvaluationStatesES,
                            recordStateEN = evaluationRecord == null ? null : evaluationRecord.EvaluationStates.NameEvaluationStatesEN,
                            applyEvaluations = ((data.cdDivisions == null || employee.Divisions.CdDivisions == data.cdDivisions)
                             && (data.idPositions.Count == 0 || data.idPositions.Any(p => p.idPosition == employee.Positions.IdPosition)))? true : false,
                        };
            return queryEmployees.ToList();
        }

        public Employees GetByIdIncludes(int id)
        {
            return _dbContext.Employees.Include(p => p.Positions).Include(d => d.Divisions).Include(s => s.Responsible)
                .Where( e => e.IdEmployees == id).FirstOrDefault();
        }
    }
}
