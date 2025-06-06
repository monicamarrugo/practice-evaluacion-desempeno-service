using EvaluacionDesempenoApi.Entities;

namespace EvaluacionDesempenoApi.DTOs
{
    public class FlagsDto
    {
        public int idFlag { get; set; }
        public string nameFlagES { get; set; }
        public string nameFlagEN { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }

        public ICollection<FlagRulesDto> flagRules { get; set; }
    }
}
