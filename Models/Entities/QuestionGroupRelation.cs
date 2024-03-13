using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("QuestionGroupRelation", Schema = "Evaluation")]
    public class QuestionGroupRelation
    {
        [Key]
        public int IdQuestionGroupRelation { get; set; }
        public int IdQuestions { get; set; }
        public int IdGroups { get; set; }
        public decimal? Weight { get; set; }
        public string? CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public Questions Questions { get; set; }
        public Groups Groups { get; set; }
    }
}
