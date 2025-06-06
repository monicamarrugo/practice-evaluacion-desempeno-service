using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("QuestionType", Schema = "Masters")]
    public class QuestionType
    {
        [Key]
        public int IdQuestionType { get; set; }
        public string CdQuestionType { get; set; }
        public string NameQuestionType { get; set; }

        [JsonIgnore]
        public ICollection<Questions> Questions { get; set; }
    }
}
