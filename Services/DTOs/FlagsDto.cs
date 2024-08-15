using EvaluacionDesempenoApi.Models.Entities;

namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class FlagsDto
    {
        public int idFlag { get; set; }
        public string nameFlag { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }

        public ICollection<FlagRulesDto> flagRules { get; set; }
    }
}
