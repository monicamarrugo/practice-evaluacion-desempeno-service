using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employees> GetAllIncludes();
        List<Employees> GetAllIncludesByResponsible(int idResponsible);
        public Employees GetByIdIncludes(int id);
    }
}
