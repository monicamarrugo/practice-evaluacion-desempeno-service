namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class ActiveEvaluationsDto
    {
        public int numPerformanceEvaluations { get; set; }
        public int numIndicadorsEvaluations { get; set; }
        public int numTotalEvaluations { get; set; }

        public List<EvaluationsDto> evaluations { get; set; }
    }
}
