namespace EvaluacionDesempenoApi.DTOs
{
    public class UserProfileDto
    {
        public int idUser { get; set; }
        public string cdProfile { get; set; }

        public string profileNameES { get; set; }
        public string profileNameEN { get; set; }

        public bool selected { get; set; }

    }
}
