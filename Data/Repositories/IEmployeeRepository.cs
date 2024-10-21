using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employees> GetAllIncludes();
        List<Employees> GetAllIncludesByResponsible(int idResponsible);
        public Employees GetByIdIncludes(int id);
        Task<List<EmployeeRecordDto>> GetEmployeesFromRecordsAsync(SearchEmployeesDto data);
        Task<List<EmployeeDto>> GetAllWithUser();
    }
}
