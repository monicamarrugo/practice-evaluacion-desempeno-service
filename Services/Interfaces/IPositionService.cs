using EvaluacionDesempenoApi.Models.Entities;
using EvaluacionDesempenoApi.Services.DTOs;

namespace EvaluacionDesempenoApi.Services.Interfaces
{
    public interface IPositionService
    {
        public List<Positions> GetAllPositions();
        List<EvaluationPositionDto> GetAllPositionsAsEvaluation();
    }
}
