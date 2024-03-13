namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class ResponseTransaction
    {
        public string error { get; set; }
        public string errorDetail { get; set; } = string.Empty;

        public string message { get; set; }

        public string response { get; set; }
    }
}
