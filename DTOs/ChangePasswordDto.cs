namespace EvaluacionDesempenoApi.DTOs
{
    public class ChangePasswordDto
    {
        public string username { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
