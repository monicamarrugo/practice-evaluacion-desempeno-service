using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IPositionService
    {
        public List<Positions> GetAllPositions();
    }
}
