namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class CreateEvaluationKpiRecordDto
    {
        public EvaluationRecordDto evaluationRecord { get; set; }
        public List<KpiRecordDetailsDto> kpiRecordDetails { get; set; }
    }
}
