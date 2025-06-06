using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.DTOs
{
    public class FileDto
    {
        public int idFile { get; set; }
        public int idEvaluationRecord { get; set; }
        public int year { get; set; }
        public string nameFile { get; set; }
        public string cdFileType { get; set; }
        public string? cdMonths { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }
    }
}
