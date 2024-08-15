using System.ComponentModel.DataAnnotations;

namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class FlagTypeDto
    {
        public int idFlagType { get; set; }
        public string cdFlagType { get; set; }
        public string flagTypeNameES { get; set; }
        public string flagTypeNameEN { get; set; }
    }
}
