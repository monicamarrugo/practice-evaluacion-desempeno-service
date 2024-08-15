using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Data.Repositories
{
    public interface IFlagRepository
    {
        void UpdateFlag(CreateFlagDto flagData);
        Flags GetFlag(int idFlag);
    }
}
