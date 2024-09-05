using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("RecordDetails", Schema = "Evaluation")]
    public class RecordDetails
    {
        [Key]
        public int IdRecordDetails { get; set;}
        public int IdEvaluationRecord { get; set;}
        public int IdQuestions { get; set;}
        public int IdGroups { get; set;}
        public string GroupNameES { get; set; }
        public string GroupNameEN { get; set; }
        public string? DescriptionES { get; set;}
        public string? DescriptionEN { get; set;}
        public string? CdQuestionType { get; set;}
        public int? Weight { get; set;}
        public string? ResponseDescription { get; set;}
        public int? Calification { get; set;}
        public decimal? TotalCalification { get; set; }
        public bool NoApplyScale { get; set;}
        public decimal? Average { get; set;}
        public int IdFlag { get; set;}
        public string FlagColor { get; set;}
        public string FlagDescription { get; set;}
        public EvaluationRecord EvaluationRecord { get; set;}
    }
}
