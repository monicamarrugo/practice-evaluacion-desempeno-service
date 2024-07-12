namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class SearchActiveEvaluationDto
    {
        public DateTimeOffset currentDate { get; set; }
        public int idProcessLeader { get; set; }

        public string cdDivisions { get; set; }
    }
}
