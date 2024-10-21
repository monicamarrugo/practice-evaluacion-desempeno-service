using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Employees", Schema = "Masters")]
    public class Employees
    {
        [Key]
        public int IdEmployees { get; set;}
        public int IdPosition { get; set;}
        public Positions Positions { get; set; }

        public string Names { get; set;}
        public string LastNames { get; set;}
        public string Sex { get; set;}
        public string? Email { get; set;}
        public int? IDResponsible { get; set;}
        public Employees Responsible { get; set; }

        public string? CdDivisions { get; set;}
        public Divisions Divisions { get; set; }
        public string? Identification { get; set;}
        public bool Enabled { get; set; }

        [JsonIgnore]
        public ICollection<Employees> Subordinates { get; set; }

        [JsonIgnore]
        public ICollection<Evaluations> Questionaries { get; set; }

        [InverseProperty("Evaluator")]
        [JsonIgnore]
        public ICollection<EvaluationRecord> EvaluationsAsEvaluator { get; set; }

        [InverseProperty("Employee")]
        [JsonIgnore]
        public ICollection<EvaluationRecord> EvaluationsAsEmployee { get; set; }
    }
}
