using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class PositionService : IPositionService
    {
        private readonly IGenericRepository<Positions> _repository;
        private readonly IMapper _mapper;

        public PositionService(IGenericRepository<Positions> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<Positions> GetAllPositions()
        {
            var entities = _repository.GetAll();
            return entities.ToList();
        }

        public List<EvaluationPositionDto> GetAllPositionsAsEvaluation()
        {
            var entities = _repository.GetAll();
            var positions = _mapper.Map<List<EvaluationPositionDto>>(entities);
            return positions;
        }
    }
}
