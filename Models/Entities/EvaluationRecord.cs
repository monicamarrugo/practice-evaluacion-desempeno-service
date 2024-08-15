using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("EvaluationRecord", Schema = "Evaluation")]
    public class EvaluationRecord
    {
        [Key]
        public int IdEvaluationRecord { get; set;}
        public int IdEvaluator { get; set;}
        public int? IdEmployee { get; set;}
        public int IdEvaluations { get; set;}
        public DateTimeOffset? StartDate { get; set;}
        public DateTimeOffset? EndDate { get; set;}
        public string? CdEvaluationStates { get; set;}
        public decimal? FinalCalification { get; set;}

        public string Language { get; set;}
        public string? EvaluationFile { get; set;}
        public string? FileType { get; set;}
        public string? CreateUser { get; set;}
        public DateTimeOffset? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTimeOffset? ModifiedDate { get; set; }

        public int? IdFlag { get; set;}

        public string? DescriptionFlag { get; set;}

        public string? ColorFlag { get; set;}
        public Employees Evaluator { get; set; }
        public Employees Employee { get; set; }
        public Evaluations Evaluation { get; set; }
        public EvaluationStates EvaluationStates { get; set; }

        [JsonIgnore]
        public ICollection<RecordDetails> RecordDetails { get; set; }

        [JsonIgnore]
        public ICollection<RecordDetailsTemp> RecordDetailsTemp { get; set; }

        [JsonIgnore]
        public ICollection<KpiRecordDetails> KpiRecordDetails { get; set; }

    }
}
