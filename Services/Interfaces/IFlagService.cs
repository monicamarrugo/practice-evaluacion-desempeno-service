using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IFlagService
    {
        ResponseTransaction SaveFlag(FlagsDto flagData);
        ResponseTransaction UpdateFlag(FlagsDto flagData);

        List<FlagsDto> GetFlags();

        FlagsDto GetFlagById(int idFlag);
    }
}
