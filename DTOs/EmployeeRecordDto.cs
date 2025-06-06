namespace EvaluacionDesempenoApi.DTOs
{
    public class EmployeeRecordDto
    {
        public int idEmployee { get; set; }
        public string names { get; set;}
        public string lastNames { get; set;}
        public string identification { get; set;}

        public string email { get; set;} 
        public string nameDivisions { get; set;}
        public string namePosition { get; set;}
        public string? nameArea { get; set; }
        public string? cdArea { get; set; }
        public string? cdDivisions { get; set; }
        public int idPosition { get; set; }
        public bool existsRecord { get; set; }

        public int? idRecordEvaluation { get; set; }
        public string cdRecordState { get; set; }
        public string? recordStateES { get; set; }
        public string? recordStateEN { get; set; }
        public int? idFlag { get; set; }

        public string? descriptionFlag { get; set; }

        public string? colorFlag { get; set; }

        public decimal? finalCalification { get; set; }
        public bool applyEvaluations { get; set; }
    }
}
