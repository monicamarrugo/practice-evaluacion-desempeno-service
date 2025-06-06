namespace EvaluacionDesempenoApi.DTOs
{
    public class RecordDetailsDto
    {
        public int? idRecordDetails { get; set; }
        public int? idEvaluationRecord { get; set; }
        public int idQuestions { get; set; }
        public int idGroups { get; set; }
        public string groupNameES { get; set; }
        public string groupNameEN { get; set; }
        public string? descriptionES { get; set; }
        public string? descriptionEN { get; set; }
        public string? cdQuestionType { get; set; }
        public int? weight { get; set; }
        public string? responseDescription { get; set; }
        public int? calification { get; set; }
        public decimal? totalCalification { get; set; }
        public bool noApplyScale { get; set; }
        public decimal? average { get; set; }
        public int idFlag { get; set; }
        public string flagColor { get; set; }
        public string flagDescription { get; set; }
    }
}
