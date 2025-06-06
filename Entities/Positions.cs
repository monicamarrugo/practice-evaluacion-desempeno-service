using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Positions", Schema = "Masters")]
    public class Positions
    {
        [Key]
        public int IdPosition { get; set;}
        public string NamePosition { get; set; }

        [JsonIgnore]
        public ICollection<Employees> Employees { get; set; }

        [JsonIgnore]
        public ICollection<EvaluationsPositions> EvaluationsPositions { get; set; }


        [JsonIgnore]
        public ICollection<EvaluationRecord> EvaluationRecord { get; set; }
    }
}
