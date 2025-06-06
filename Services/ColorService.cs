using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class ColorService : IColorService
    {
        private readonly IGenericRepository<Colors> _colorsGenericRepository;
        private readonly IMapper _mapper;

        public ColorService(IGenericRepository<Colors> colorsGenericRepository,
            IMapper mapper )
        {
            _colorsGenericRepository = colorsGenericRepository;
            _mapper = mapper;
        }
        public List<ColorDto> GetColors()
        {
            try
            {
                List<ColorDto> colorDtos = new List<ColorDto>();
                var entities = _colorsGenericRepository.GetAll();
                colorDtos = _mapper.Map<List<ColorDto>>( entities );
                return colorDtos;
            }

            catch( Exception ex )
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
