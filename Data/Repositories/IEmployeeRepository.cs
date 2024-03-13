using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employees> GetAllIncludes();
        public Employees GetByIdIncludes(int id);
    }
}
