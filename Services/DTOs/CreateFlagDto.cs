namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class CreateFlagDto
    {
        public FlagsDto flags { get; set; }
        public List<FlagRulesDto> flagRules { get; set; }
    }
}
