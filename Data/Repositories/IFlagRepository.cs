using EvaluacionDesempenoApi.DTOs;
using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IFlagRepository
    {
        void UpdateFlagRules(FlagsDto flagData);
        Flags GetFlag(int idFlag);
    }
}
