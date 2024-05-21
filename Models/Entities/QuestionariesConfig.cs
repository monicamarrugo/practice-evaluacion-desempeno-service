using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("QuestionariesConfig", Schema = "Evaluation")]
    public class QuestionariesConfig
    {
        [Key]
        public int IdQuestionaryConfig { get; set;}
        public int IdQuestionary { get; set;}
        public int IdQuestions { get; set;}
        public bool NoApplyScale { get; set; }

        public int? Weight { get; set; }

        public string? Control { get; set; }

        public string? DisplayFormats { get; set; }

        public string? Cdfrequency { get; set; } 
                

        public int? Incentive { get; set; }

        public Questionaries Questionary { get; set; }
        public Questions Questions { get; set; }

        public Frequencies Frequencies { get; set; }

        
    }
}
