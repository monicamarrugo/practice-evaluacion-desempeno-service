using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Enums;
using EvaluacionDesempenoApi.Services.Interfaces;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employees> _employeesGenericRepository;
        private readonly IEmployeeRepository _employeesRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeesRepository, IGenericRepository<Employees> employeesGenericRepository,
            IMapper mapper)
        {
            _employeesRepository = employeesRepository;
            _employeesGenericRepository = employeesGenericRepository;
            _mapper = mapper;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var entities = _employeesRepository.GetAllIncludes();
            employees = _mapper.Map<List<EmployeeDto>>(entities);
            return employees;
        }
        public async Task<List<EmployeeDto>> GetAllEmployeesWithUsers()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            employees = await _employeesRepository.GetAllWithUser();
            return employees;
        }

        public List<EmployeeDto> GetAllEmployeesByResponsible(int idResponsible)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var entities = _employeesRepository.GetAllIncludesByResponsible(idResponsible);
            employees = _mapper.Map<List<EmployeeDto>>(entities);
            return employees;
        }

        public List<EmployeeDto> GetEmployeesEvaluations(SearchEmployeesDto data)
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var entities = _employeesRepository.GetAllIncludesByResponsible(data.idResponsible);
            employees = _mapper.Map<List<EmployeeDto>>(entities);
            employees.ForEach(e => {
                if ((data.cdDivisions == null || e.cdDivisions == data.cdDivisions) 
                && (data.idPositions.Count == 0 || data.idPositions.Any( p => p.idPosition == e.idPosition)))
                {
                    e.applyEvaluations = true;
                }
            });
            return employees;
        }
        public async Task<DashboardEvaluationOneDto> GetEmployeesFromRecordsAsync(SearchEmployeesDto data)
        {
            DashboardEvaluationOneDto dashboardEvaluationOneDto = new DashboardEvaluationOneDto();
            var employees= await _employeesRepository.GetEmployeesFromRecordsAsync(data);
            var totalEmployees =  employees.Count;
            var totalApplies = employees.Count(e => e.applyEvaluations == true);
            var totalDone = employees.Count(e => e.existsRecord == true && e.cdRecordState == RecordStateEnum.Finished.GetStringValue());

            dashboardEvaluationOneDto.totalEmployees = totalEmployees;
            dashboardEvaluationOneDto.totalEnabled = totalApplies;
            dashboardEvaluationOneDto.totalDone = totalDone;
            dashboardEvaluationOneDto.employees = employees;
            return dashboardEvaluationOneDto;

        }

            public EmployeeDto GetById(int id)
        {
            EmployeeDto employee = new EmployeeDto();
            var entity = _employeesRepository.GetByIdIncludes(id);

            employee.idEmployees = entity.IdEmployees;
            employee.idPosition = entity.IdPosition;
            employee.namePosition = entity.Positions.NamePosition;
            employee.names = entity.Names;
            employee.lastNames = entity.LastNames;
            employee.sex = entity.Sex;
            employee.email = entity.Email;
            employee.iDResponsible = entity.IDResponsible != null ? entity.IDResponsible.Value: null;
            employee.cdDivisions = entity.CdDivisions;
            employee.nameDivisions = entity.Divisions.Name;
            employee.identification = entity.Identification;
            employee.enabled = entity.Enabled;
            
            return employee;

        }

        public ResponseTransaction SaveEmployee(EmployeeDto employee)
        {
            ResponseTransaction response = new ResponseTransaction();
            if(employee == null) {

                response.error = "SI";
                response.errorDetail = "Faltan datos del empleado";
                return response;
            }
            try
            {
                Employees Employee = new Employees()
                {
                    
                    IdPosition = employee.idPosition,
                    Names = employee.names,
                    LastNames = employee.lastNames,
                    Sex = employee.sex,
                    Email = employee.email,
                    IDResponsible = employee.iDResponsible,
                    CdDivisions = employee.cdDivisions,
                    Identification = employee.identification,
                    Enabled = true
                };

                _employeesGenericRepository.Add(Employee);
                response.error = "NO";
                response.message = "El registro fue creado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

        public ResponseTransaction UpdatelEmployee(EmployeeDto employee)
        {
            ResponseTransaction response = new ResponseTransaction();
            try
            {
                Employees Employee = new Employees()
                {
                    IdEmployees = employee.idEmployees.Value,
                    IdPosition = employee.idPosition,
                    Names = employee.names,
                    LastNames = employee.lastNames,
                    Sex = employee.sex,
                    Email = employee.email,
                    IDResponsible = employee.iDResponsible,
                    CdDivisions = employee.cdDivisions,
                    Identification = employee.identification,
                    Enabled = employee.enabled
                };

                _employeesGenericRepository.Update(Employee);
                response.error = "NO";
                response.message = "El registro fue actualizado exitosamente!";
                return response;
            }
            catch (Exception ex)
            {
                response.error = "SI";
                response.errorDetail = ex.Message;
                return response;
            }
        }

    }
}
