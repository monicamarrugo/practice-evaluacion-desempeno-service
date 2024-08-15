using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeDto> GetAllEmployees();

        List<EmployeeDto> GetAllEmployeesByResponsible(int idResponsible);
        public ResponseTransaction SaveEmployee(EmployeeDto employee);
        public ResponseTransaction UpdatelEmployee(EmployeeDto employee);
        public EmployeeDto GetById(int id);
        List<EmployeeDto> GetEmployeesEvaluations(SearchEmployeesDto data);

        Task<DashboardEvaluationOneDto> GetEmployeesFromRecordsAsync(SearchEmployeesDto data);
    }
}
