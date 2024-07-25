namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class KpiRecordDetailsDto
    {
        public int idKpiRecordDetails { get; set; }
        public int idEvaluationRecord { get; set; }
        public int? weight { get; set; }
        public string? control { get; set; }
        public string? displayFormats { get; set; }

        public string? cdFormats { get; set; }
        public string? cdfrequency { get; set; }
        public decimal? incentive { get; set; }
        public string? nameES { get; set; }
        public string? nameEN { get; set; }
        public string cdMonths { get; set; }
        public string realValue { get; set; }
        public bool won { get; set; }
        public decimal earned { get; set; }
        public string? kpiFile { get; set; }
        public string? fileType { get; set; }
    }
}
