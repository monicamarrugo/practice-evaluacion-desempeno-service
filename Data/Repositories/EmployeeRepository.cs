using EvaluacionDesempenoApi.Data.Context;
using EvaluacionDesempenoApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Employees> GetAllIncludes()
        {
            return _dbContext.Employees.Include(p => p.Positions)
                .Include(d => d.Divisions).Include(s => s.Responsible).ToList();
        }

        public Employees GetByIdIncludes(int id)
        {
            return _dbContext.Employees.Include(p => p.Positions).Include(d => d.Divisions).Include(s => s.Responsible)
                .Where( e => e.IdEmployees == id).FirstOrDefault();
        }
    }
}
