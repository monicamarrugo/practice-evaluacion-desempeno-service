using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class EmployeeDto
    {
        public int? idEmployees { get; set; }
        public int idPosition { get; set; }

        public string? namePosition { get; set; }
        public string names { get; set; }
        public string lastNames { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public int? iDResponsible { get; set; }
        public string? nameResponsible { get; set; }
        public string cdDivisions { get; set; }
        public string? nameDivisions { get; set; }
        public string identification { get; set; }
        public bool enabled { get; set; }

        public bool applyEvaluations { get; set; }
    }
}
