using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.DTOs
{
    public class EvaluatorRecordDto
    {
        public int evaluatorId { get; set; }
        public string evaluatorName { get; set; }

        public string positionName { get; set; }
        public int totalSubordinates { get; set; }
        public int totalEnables { get; set; }

        public int totalDone { get; set; }

        public List<EvaluationRecordDto> evaluationRecords { get; set; }
    }
}
