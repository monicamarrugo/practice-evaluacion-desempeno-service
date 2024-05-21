namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class EvaluationCreateDto
    {
        public EvaluationsDto evaluation { get; set; }
        public List<EvaluationPositionDto> evaluationPosition { get; set; }
    }
}
