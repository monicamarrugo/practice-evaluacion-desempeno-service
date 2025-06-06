using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EvaluacionDesempenoApi.DTOs
{
    public class QuestionariesConfigDto
    {
        public int? idQuestionaryConfig { get; set; }
        public int? idQuestionary { get; set; }

        public int idQuestions { get; set; }

        public int? idQuestionGroupRelation { get; set; }

        public string? nameQuestionES { get; set; }

        public string? nameQuestionEN { get; set; }

        public string? descriptionQuestionES { get; set; }

        public string? descriptionQuestionEN { get; set; }

        public int? idGroups { get; set; }


        public string? groupNameES { get; set; }

        public string? groupNameEN { get; set; }

        public string? cdArea { get; set; }
        public string? nameArea { get; set; }

        public int? incentive { get; set; }

        public bool? noApplyScale { get; set; }
        public int? weight { get; set; }
        public string? control { get; set; }
        public string? displayFormats { get; set; }
        public string? cdFrequency { get; set; }

    }
}
