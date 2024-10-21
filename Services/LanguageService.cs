using AutoMapper;
using EvaluacionDesempenoApi.Data.Repositories;
using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;
using EvaluacionDesempenoApi.Services.Interfaces;

namespace EvaluacionDesempenoApi.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IGenericRepository<Languages> _repository;
        private readonly IMapper _mapper;
        public LanguageService(IGenericRepository<Languages> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public List<LanguageDto> GetAllLanguages()
        {
            List<LanguageDto> languages = new List<LanguageDto>();
            var entities = _repository.GetAll();
            languages = _mapper.Map<List<LanguageDto>>(entities);
            return languages;
        }
    }
}
