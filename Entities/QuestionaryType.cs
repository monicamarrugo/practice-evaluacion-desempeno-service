using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("QuestionaryTypes", Schema = "Masters")]
    public class QuestionaryTypes
    {
        public int IdQuestionaryType { get; set;}

        [Key]
        public string CdQuestionaryType { get; set;}
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Questionaries> Questionaries { get; set; }
    }
}
