using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Questionaries", Schema = "Evaluation")]
    public class Questionaries
    {
        [Key]
        public int IdQuestionary { get; set;}
        public string Name { get; set;}
        public string CdQuestionaryType { get; set;}
        public string? CdArea { get; set; }

        public QuestionaryTypes QuestionaryTypes { get; set;}
      
        public string? CreateUser { get; set;}
        public DateTime? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public ICollection<QuestionariesConfig> QuestionariesConfig { get; set; }

        public Areas Area { get; set; }

        [JsonIgnore]
        public ICollection<Evaluations> Evaluations { get; set; }
    }
}
