namespace EvaluacionDesempenoApi.DTOs
{
    public class QuestionDto
    {
        public int idQuestion { get; set; }
        public int idQuestionType { get; set; }
        public int? idGroups { get; set; }
        public int? idQuestionGroup { get; set; }
        public string groupNameES { get; set; }
        public string groupNameEN { get; set; }


        public string? nameES { get; set; }
        public string? nameEN { get; set; }
        public string descriptionES { get; set; }
        public string descriptionEN { get; set; }

        public string? cdArea { get; set; }
        public string? nameArea { get; set; }
        public int? idFormats { get; set; }
    }
}
