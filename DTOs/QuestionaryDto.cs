namespace EvaluacionDesempenoApi.DTOs
{
    public class QuestionaryDto
    {
        public int? idQuestionary { get; set; }
        public string name { get; set; }
        public string cdQuestionaryType { get; set; }
        public string? nameQuestionaryType { get; set; }
        public string? cdArea { get; set; }
        public string? nameArea { get; set; }

        public int? iDProcessLeader { get; set; }

        public string? createUser { get; set; }
        public DateTime? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTime? modifiedDate { get; set; }
    }
}
