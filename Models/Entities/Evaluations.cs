using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Evaluations", Schema = "Evaluation")]
    public class Evaluations
    {
        [Key]
        public int IdEvaluations { get; set;}
        public int IdQuestionary { get; set;}
        public string Title { get; set;}
        public int? IdEscales { get; set;}
        public string? CdDivisions { get; set;}
        public string? CdTypeEvaluation { get; set; }
        public DateTimeOffset? StartDate { get; set;}
        public DateTimeOffset? EndDate { get; set;}
        public int? Year { get; set;}
        public int? IDProcessLeader { get; set; }
        public bool? IndApplyEB { get; set;}
        public bool? IndEnabled { get; set;}
        public string? CreateUser { get; set;}
        public DateTimeOffset? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTimeOffset? ModifiedDate { get; set; }

        public Escales Escales { get; set; }
        public Divisions Divisions { get; set; }
        public Questionaries Questionaries { get; set; }
        public Employees Employees { get; set; }

        [JsonIgnore]
        public ICollection<EvaluationsPositions> EvaluationsPositions { get; set; }

        
    }
}
