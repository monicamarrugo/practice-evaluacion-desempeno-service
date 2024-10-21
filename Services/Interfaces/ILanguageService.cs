using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface ILanguageService
    {
        public List<LanguageDto> GetAllLanguages();
    }
}

