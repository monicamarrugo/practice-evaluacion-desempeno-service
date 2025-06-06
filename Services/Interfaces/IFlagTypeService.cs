using EvaluacionDesempenoApi.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IFlagTypeService
    {
        List<FlagTypeDto> GetFlagTypes();
    }
}
