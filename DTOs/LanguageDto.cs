using System.ComponentModel.DataAnnotations;

namespace EvaluacionDesempenoApi.DTOs
{
    public class LanguageDto
    {
        public int idLanguage { get; set; }
        public string cdLanguage { get; set; }
        public string languageName { get; set; }
    }
}
