using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class PositionService : IPositionService
    {
        private readonly IGenericRepository<Positions> _repository;

        public PositionService(IGenericRepository<Positions> repository)
        {
            _repository = repository;
        }
        public List<Positions> GetAllPositions()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }
    }
}
