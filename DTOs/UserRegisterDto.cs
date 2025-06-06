using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.DTOs
{
    public class UserRegisterDto
    {
        public int idEmployee { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string? altName { get; set; }
        public string? altEmail { get; set; }
        public string cdLanguage { get; set; }

        public bool indChangePassword { get; set; }
        public bool indEnabled { get; set; }

        public List<UserProfileDto> usersProfiles { get; set; }
    }
}
