using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class AreaService : IAreaService
    {
        private readonly IGenericRepository<Areas> _repository;

        public AreaService(IGenericRepository<Areas> repository)
        {
            _repository = repository;
        }
        public List<Areas> GetAllAreas()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }
    }
}
