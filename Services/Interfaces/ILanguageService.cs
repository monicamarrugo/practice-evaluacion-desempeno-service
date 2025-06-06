using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface ILanguageService
    {
        public List<LanguageDto> GetAllLanguages();
    }
}

