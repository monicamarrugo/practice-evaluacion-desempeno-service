namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class EvaluationsDto
    {
        public int idEvaluations { get; set; }
        public int idQuestionary { get; set; }

        public string questionaryName { get; set; }
        public string title { get; set; }
        public int? idEscales { get; set; }
        public string? cdDivisions { get; set; }
        public DateTimeOffset? startDate { get; set; }
        public DateTimeOffset? endDate { get; set; }
        public int? year { get; set; }
        public bool? indApplyEB { get; set; }
        public bool? indEnabled { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }
    }
}
