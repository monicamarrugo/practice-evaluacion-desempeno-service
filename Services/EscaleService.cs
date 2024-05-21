using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class EscaleService : IEscaleService
    {
        private readonly IGenericRepository<Escales> _repository;

        public EscaleService(IGenericRepository<Escales> repository)
        {
            _repository = repository;
        }
        public List<Escales> GetAllEscales()
        {
            var entities = _repository.GetAll();

            return entities.ToList();
        }

        public List<EscalesValues> GetAllEscalesValues()
        {
            throw new NotImplementedException();
        }
    }
}
