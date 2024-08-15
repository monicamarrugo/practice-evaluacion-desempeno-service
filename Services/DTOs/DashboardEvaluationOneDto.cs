namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class DashboardEvaluationOneDto
    {  
        public int totalEmployees { get; set; }
        public int totalEnabled { get; set; }

        public int totalDone { get; set; }
        public List<EmployeeRecordDto> employees{ get; set; }

    }
}
