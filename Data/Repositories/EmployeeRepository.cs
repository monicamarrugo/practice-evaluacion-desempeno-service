using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeRepository(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        public List<Employees> GetAllIncludes()
        {
            return _dbContext.Employees.Include(p => p.Positions)
                .Include(d => d.Divisions).Include(s => s.Responsible).ToList();
        }
        public async Task<List<EmployeeDto>> GetAllWithUser()
        {
            var employeeDtos = await (
               from employee in _dbContext.Employees
               join user in _userManager.Users on employee.IdEmployees equals user.IdEmployee into userGroup
               from user in userGroup.DefaultIfEmpty()  // Left join para empleados sin usuario
               select new EmployeeDto
               {
                   idEmployees = employee.IdEmployees,
                   idPosition = employee.IdPosition,
                   namePosition = employee.Positions.NamePosition,
                   names = employee.Names,
                   lastNames = employee.LastNames,
                   sex = employee.Sex,
                   email = employee.Email,
                   iDResponsible = employee.IDResponsible,
                   nameResponsible = employee.Responsible != null ? employee.Responsible.Names : null,
                   cdDivisions = employee.Divisions.CdDivisions,
                   nameDivisions = employee.Divisions.Name,
                   identification = employee.Identification,
                   enabled = employee.Enabled,
                   userId = user != null ? user.Id : (int?)null
               }
           ).ToListAsync();

            return employeeDtos;
        }
        public List<Employees> GetAllIncludesByResponsible(int idResponsible)
        {
            return _dbContext.Employees.Include(p => p.Positions)
                .Include(d => d.Divisions)
                .Where(e => e.IDResponsible == idResponsible && e.Enabled == true)
                .ToList();
        }

        public Task<List<EmployeeRecordDto>> GetEmployeesFromRecordsAsync(SearchEmployeesDto data)
        {
            var idPositionsList = data.idPositions.Select(p => p.idPosition).ToList();
            var queryEmployees = from employee in _dbContext.Employees
                        .Include(e => e.Divisions)
                        .Include(e => e.Positions)
                        where employee.IDResponsible == data.idResponsible && employee.Enabled
                        join evaluationRecord in _dbContext.EvaluationRecord
                            .Include(er => er.EvaluationStates)
                            .Where(er => er.IdEvaluations == data.idEvaluation)
                        on employee.IdEmployees equals evaluationRecord.IdEmployee into evaluationGroup
                        from evaluationRecord in evaluationGroup.DefaultIfEmpty()
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
                             && (data.idPositions.Count == 0 || idPositionsList.Any(p => p == employee.IdPosition)))? true : false,
                            idFlag = evaluationRecord == null ? null : evaluationRecord.IdFlag,
                            descriptionFlag = evaluationRecord == null ? null : evaluationRecord.DescriptionFlag,
                            colorFlag = evaluationRecord == null ? null : evaluationRecord.ColorFlag,
                            finalCalification = evaluationRecord == null ? null :evaluationRecord.FinalCalification,
                        };
            return queryEmployees.ToListAsync();
        }

        public Employees GetByIdIncludes(int id)
        {
            return _dbContext.Employees.Include(p => p.Positions).Include(d => d.Divisions).Include(s => s.Responsible)
                .Where( e => e.IdEmployees == id).FirstOrDefault();
        }
    }
}
