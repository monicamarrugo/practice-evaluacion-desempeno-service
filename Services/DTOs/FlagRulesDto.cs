namespace EvaluacionDesempenoApi.Services.DTOs
{
    public class FlagRulesDto
    {
        public int idFlagRules { get; set; }
        public int idFlag { get; set; }
        public string cdFlagType { get; set; }
        public string descriptionES { get; set; }
        public string descriptionEN { get; set; }
        public decimal? minValue { get; set; }
        public decimal? maxValue { get; set; }
        public decimal? value { get; set; }
        public int idColor { get; set; }

        public string colorCode { get; set; }
        public string? createUser { get; set; }
        public DateTimeOffset? createDate { get; set; }
        public string? modifiedUser { get; set; }
        public DateTimeOffset? modifiedDate { get; set; }
    }
}
