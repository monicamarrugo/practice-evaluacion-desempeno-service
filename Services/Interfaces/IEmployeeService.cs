using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IEmployeeService
    {
        public List<EmployeeDto> GetAllEmployees();
        public ResponseTransaction SaveEmployee(EmployeeDto employee);
        public ResponseTransaction UpdatelEmployee(EmployeeDto employee);
        public EmployeeDto GetById(int id);
    }
}
