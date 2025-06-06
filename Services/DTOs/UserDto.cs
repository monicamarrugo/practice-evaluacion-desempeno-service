namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class UserDto
    {
        public int id { get; set; }
        public int? idEmployee { get; set; }
        public string username { get; set; }
        public string altName { get; set; }
        public string altEmail { get; set; }
        public string cdLanguage { get; set; }
        public bool indEnabled { get; set; }
        public bool indChangePassword { get; set; }
    }
}
