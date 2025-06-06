using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                .Include(d => d.Divisions).Include(s => s.Responsible)
                .Include(a => a.Areas).ToList();
        }
        public async Task<List<EmployeeDto>> GetAllWithUser(int pageNumber, int pageSize)
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
                   nameArea = employee.Areas.NameArea,
                   names = employee.Names,
                   lastNames = employee.LastNames,
                   sex = employee.Sex,
                   email = employee.Email != null? employee.Email: string.Empty ,
                   iDResponsible = employee.IDResponsible,
                   nameResponsible = employee.Responsible != null ? string.Format("{0} {1}",employee.Responsible.Names, employee.Responsible.LastNames) : null,
                   cdDivisions = employee.Divisions.CdDivisions,
                   nameDivisions = employee.Divisions.Name,
                   identification = employee.Identification!,
                   enabled = employee.Enabled,
                   userId = user != null ? user.Id : (int?)null
               }
           )
           .OrderBy(e => e.names)
           .ToListAsync();

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
                        .Include(e => e.Areas)
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
                            nameArea = employee.Areas.NameArea,
                            cdArea = employee.Areas.CdArea,
                            nameDivisions = employee.Divisions.Name,
                            cdDivisions = employee.Divisions.CdDivisions,
                            namePosition = employee.Positions.NamePosition,
                            idPosition = employee.Positions.IdPosition,
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
        public async Task<PaginatedList<EvaluatorRecordDto>> GetEvaluatorRecords(SearchEmployeesDto data)
        {
            var idPositionsList = data.idPositions.Select(p => p.idPosition).ToList();

            // Cargar empleados con los filtros básicos
            var qemployees = _dbContext.Employees
                .Include(e => e.Subordinates)
                .Include(e => e.Positions)
                .Where(e => e.Subordinates.Any(sub => sub.Enabled));
                //.OrderBy(e => e.IdEmployees); // Ordenar explícitamente
                                              // Cargar todos los empleados relevantes

            if (!string.IsNullOrEmpty(data.nameEvaluator))
            {
                qemployees = qemployees.Where(er =>
                    (er.Names + " " + er.LastNames).Contains(data.nameEvaluator));
            }

            if (!string.IsNullOrEmpty(data.identificationEvaluator))
            {
                qemployees = qemployees.Where(er => er.Identification == data.identificationEvaluator);
            }


            var employees = await qemployees.ToListAsync();
            var totalRecords = employees.Count();

            // Paginación en memoria porque sql server 2008 R2 no soporta los comandos de paginación de EF CORE 6
            var paginatedEmployees = employees
                .Skip((data.pageNumber - 1) * data.pageSize)
                .Take(data.pageSize)
                .ToList();

            // Obtener los Ids de empleados seleccionados
            var employeeIds = paginatedEmployees.Select(e => e.IdEmployees).ToList();

            // Cargar las evaluaciones relacionadas con esos empleados
            var evaluations = await _dbContext.EvaluationRecord
                .Where(er => employeeIds.Contains(er.IdEvaluator) && er.IdEvaluations == data.idEvaluation)
                .ToListAsync();

            // Procesar los datos en memoria
            var result = paginatedEmployees
                .Select(employee =>
                {
                    var employeeEvaluations = evaluations.Where(e => e.IdEvaluator == employee.IdEmployees).ToList();
                    return new EvaluatorRecordDto
                    {
                        evaluatorId = employee.IdEmployees,
                        evaluatorName = $"{employee.Names} {employee.LastNames}",
                        positionName = employee.Positions.NamePosition, 
                        totalSubordinates = employee.Subordinates.Count(s=> s.Enabled),
                        totalEnables = employee.Subordinates.Count(sub =>
                            (data.cdDivisions == null || sub.CdDivisions == data.cdDivisions) &&
                            (data.idPositions.Count == 0 || idPositionsList.Contains(sub.IdPosition)) && sub.Enabled),
                        totalDone = employeeEvaluations.Count(e => e.CdEvaluationStates == RecordStateEnum.Finished.GetStringValue()),
                        evaluationRecords = employeeEvaluations.Select(evaluation => new EvaluationRecordDto
                        {
                            idEvaluationRecord = evaluation.IdEvaluationRecord,
                            startDate = evaluation.StartDate,
                            startDateLocal = evaluation.StartDateLocal,
                            endDate = evaluation.EndDate,
                            endDateLocal = evaluation.EndDateLocal,
                            finalCalification = evaluation.FinalCalification,
                            cdEvaluationStates = evaluation.CdEvaluationStates,
                            idEmployee = evaluation.IdEmployee,
                            nameEmployee = $"{evaluation.Employee?.Names} {evaluation.Employee?.LastNames}",
                        }).ToList()
                    };
                })
                .ToList();

            var paginatedResult = new PaginatedList<EvaluatorRecordDto>(result, totalRecords, data.pageNumber, data.pageSize);

            return paginatedResult;
        }
       

        public async void GetEmployeesWithGroupedEvaluationsCopy(SearchEmployeesDto data)
        {
            var idPositionsList = data.idPositions.Select(p => p.idPosition).ToList();

            var employeesWithGroupedEvaluations = await _dbContext.Employees
                 .Include(e => e.Divisions)
                        .Include(e => e.Positions)
                        .Include(e => e.Areas)
                .Where(e => e.Subordinates.Any()) // Filtra empleados con subordinados
                .GroupJoin(
                    _dbContext.EvaluationRecord.Include(er => er.Employee).Where(er => er.IdEvaluations == data.idEvaluation),
                    e => e.IdEmployees,
                    er => er.IdEvaluator,
                    (employee, evaluations) => new
                    {
                        employeeId = employee.IdEmployees,
                        employeeName = $"{employee.Names} {employee.LastNames}",
                        totalSubordinates = employee.Subordinates.Count(),
                        totalEnables = employee.Subordinates.Count(sub => (data.cdDivisions == null || sub.CdDivisions == data.cdDivisions) &&
                            (data.idPositions.Count == 0 || idPositionsList.Any(p => p == sub.IdPosition)) ? true : false),
                        subordinates = employee.Subordinates.Select(sub => new
                        {
                            idEmployee = sub.IdEmployees,
                            names = $"{sub.Names} {sub.LastNames}",
                            nameDivisions = sub.Divisions.Name,
                            namePosition = sub.Positions.IdPosition,
                            nameArea = sub.Areas.NameArea,
                            applyEvaluations = (data.cdDivisions == null || sub.CdDivisions == data.cdDivisions) &&
                            (data.idPositions.Count == 0 || idPositionsList.Any(p => p == sub.IdPosition)) ? true : false
                        }).ToList(),
                        evaluationRecords = evaluations.Select(er => new
                        {
                            idEvaluationRecord = er.IdEvaluationRecord,
                            startDate = er.StartDate,
                            startDateLocal = er.StartDateLocal,
                            endDate = er.EndDate,
                            endDateLocal = er.EndDateLocal,
                            finalCalification = er.FinalCalification,
                            cdEvaluationStates = er.CdEvaluationStates,
                            idEmployee = er.IdEmployee,
                            nameEmployee = $"{er.Employee.Names} {er.Employee.LastNames}",
                        }).ToList()
                    })
                .ToListAsync();
        }
        public Employees GetByIdIncludes(int id)
        {
            return _dbContext.Employees.Include(p => p.Positions).Include(d => d.Divisions).Include(s => s.Responsible)
                .Where( e => e.IdEmployees == id).FirstOrDefault();
        }
        public bool Exists(string identification)
        {
            return _dbContext.Employees
                .Any(e => e.Identification == identification);
        }

    }
}
