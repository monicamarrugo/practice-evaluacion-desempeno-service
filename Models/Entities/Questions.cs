using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Questions", Schema = "Evaluation")]
    public class Questions
    {
        [Key]
        public int IdQuestions { get; set; }
        public int IdQuestionType { get; set; }
        public QuestionType QuestionType { get; set; }
        public int IdFormats { get; set; }
        public Formats Formats { get; set; }
        public string? NameES { get; set; }
        public string? NameEN { get; set; }
        public string? DescriptionES { get; set; }
        public string? DescriptionEN { get; set; }
        public bool Enabled { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? CdArea { get; set; }
        public Areas Areas { get; set; }

        [JsonIgnore]
        public ICollection<QuestionGroupRelation> QuestionGroupRelations { get; set; }

        [JsonIgnore]
        public ICollection<QuestionariesConfig> QuestionariesConfig { get; set; }
    }
}
