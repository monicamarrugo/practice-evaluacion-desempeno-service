namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class EvaluationPositionDto
    {
        public int idEvaluationsPositions { get; set; }
        public int idEvaluations { get; set; }
        public int idPosition { get; set; }

        public string namePosition { get; set; }

        public bool selected { get; set; }
    }
}
