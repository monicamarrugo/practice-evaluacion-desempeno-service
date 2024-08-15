using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class FlagTypeService : IFlagTypeService
    {
        private readonly IGenericRepository<FlagTypes> _flagTypesGenericRepository;
        private readonly IMapper _mapper;
        public FlagTypeService(IGenericRepository<FlagTypes> flagTypesGenericRepository,
             IMapper mapper) 
        {
            _flagTypesGenericRepository = flagTypesGenericRepository;
            _mapper = mapper;

        }
        public List<FlagTypeDto> GetFlagTypes()
        {
            try
            {
                var entities = _flagTypesGenericRepository.GetAll();
                return _mapper.Map<List<FlagTypeDto>>(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
