using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class SearchEmployeesDto
    {
        public int idResponsible { get; set; }
        public List<EvaluationPositionDto> idPositions { get; set; } 
        public string cdDivisions { get; set;}

        public int idEvaluation { get; set; }
    }
}
