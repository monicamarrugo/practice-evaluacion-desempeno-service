using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employees> _employeesGenericRepository;
        private readonly IEmployeeRepository _employeesRepository;

        public EmployeeService(IEmployeeRepository employeesRepository, IGenericRepository<Employees> employeesGenericRepository)
        {
            _employeesRepository = employeesRepository;
            _employeesGenericRepository = employeesGenericRepository;
        }

        public List<EmployeeDto> GetAllEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>();
            var entities = _employeesRepository.GetAllIncludes();

            foreach (var entity in entities)
            {
                employees.Add(new EmployeeDto()
                {
                    idEmployees = entity.IdEmployees,
                    idPosition = entity.IdPosition,
                    namePosition = entity.Positions.NamePosition,
                    names = entity.Names,
                    lastNames = entity.LastNames,
                    sex = entity.Sex,
                    email = entity.Email,
                    iDResponsible = entity.IDResponsible != null? entity.IDResponsible.Value:null,
                    nameResponsible = entity.Responsible != null ? entity.Responsible.Names +" "+ entity.Responsible.LastNames : null,
                    cdDivisions = entity.CdDivisions,
                    nameDivisions = entity.Divisions.Name,
                    identification = entity.Identification,
                    enabled = entity.Enabled
                });
            }
            return employees;
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
