using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class DivisionService : IDivisionService
    {
        private readonly IGenericRepository<Divisions> _repository;

        public DivisionService(IGenericRepository<Divisions> repository)
        {
            _repository = repository;
        }
        public List<Divisions> GetAllDivisions()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }
    }
}
