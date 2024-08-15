using EvaluacionDesempenoApi.Models.Entities;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class EvaluationRecordDto
    {

        public int idEvaluationRecord { get; set; }
        public int idEvaluator { get; set; }
        public int? idEmployee { get; set; }
        public int idEvaluations { get; set; }
        public DateTimeOffset? startDate { get; set; }
        public DateTimeOffset? endDate { get; set; }
        public string? cdEvaluationStates { get; set; }
        public decimal? finalCalification { get; set; }

        public string language { get; set; }
        public string? evaluationFile { get; set; }
        public string? fileType { get; set; }

        public int? idFlag { get; set; }

        public string? descriptionFlag { get; set; }

        public string? colorFlag { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }

    }
}
