using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("EvaluationsPositions", Schema = "Evaluation")]
    public class EvaluationsPositions
    {
        [Key]
        public int IdEvaluationsPositions { get; set;}
        public int IdEvaluations { get; set;}
        public int IdPosition { get; set; }

        public Evaluations Evaluations { get; set;}

        public Positions Positions { get; set;}
    }
}
