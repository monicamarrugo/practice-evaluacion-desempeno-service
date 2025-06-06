using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employees> GetAllIncludes();
        List<Employees> GetAllIncludesByResponsible(int idResponsible);
        public Employees GetByIdIncludes(int id);
        Task<List<EmployeeRecordDto>> GetEmployeesFromRecordsAsync(SearchEmployeesDto data);
        Task<List<EmployeeDto>> GetAllWithUser(int pageNumber, int pageSize);
        bool Exists(string identification);
        Task<PaginatedList<EvaluatorRecordDto>> GetEvaluatorRecords(SearchEmployeesDto data);
    }
}
