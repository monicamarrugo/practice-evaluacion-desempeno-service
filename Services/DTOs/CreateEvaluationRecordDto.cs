namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class CreateEvaluationRecordDto
    {
        public EvaluationRecordDto evaluationRecord { get; set; }
        public List<RecordDetailsDto> recordDetails { get; set; }
    }
}
