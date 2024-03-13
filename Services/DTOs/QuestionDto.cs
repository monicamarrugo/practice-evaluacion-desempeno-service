namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class QuestionDto
    {
        public int idQuestion { get; set; }
        public int idQuestionType { get; set; }
        public int? idGroups { get; set; }
        public int? idQuestionGroup { get; set; }
        public string groupName { get; set; }


        public string? nameES { get; set; }
        public string? nameEN { get; set; }
        public string descriptionES { get; set; }
        public string descriptionEN { get; set; }

        public int? value { get; set; }
        public string? control { get; set; }
        public int? idFormats { get; set; }
        public string? displayFormats { get; set; }
    }
}
