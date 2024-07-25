using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("EvaluationStates", Schema = "Masters")]
    public class EvaluationStates
    {
        public int IdEvaluationStates { get; set;}

        [Key]
        public string CdEvaluationStates { get; set;}
        public string NameEvaluationStatesES { get; set; }
        public string NameEvaluationStatesEN { get; set; }

        [JsonIgnore]
        public ICollection<EvaluationRecord> EvaluationRecords { get; set; }
    }
}
