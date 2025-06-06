using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EvaluacionDesempenoApi.DTOs
{
    public class SearchEmployeesDto
    {
        public int? idResponsible { get; set; }
        public List<EvaluationPositionDto> idPositions { get; set; }
        public string? cdDivisions { get; set; }

        public int idEvaluation { get; set; }
        public string nameEvaluator { get; set; }
        public string identificationEvaluator { get; set; }

        public int pageNumber { get; set; }

        public int pageSize { get; set; }
    }
}
