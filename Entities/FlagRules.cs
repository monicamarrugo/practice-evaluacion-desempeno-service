using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("FlagRules", Schema = "Evaluation")]
    public class FlagRules
    {
        [Key]
        public int IdFlagRules { get; set;}
        public int IdFlag { get; set;}
        public string CdFlagType { get; set;}
        public string DescriptionES { get; set;}
        public string DescriptionEN { get; set; }
        public decimal? MinValue { get; set;}
        public decimal? MaxValue { get; set;}
        public decimal? Value { get; set;}
        public int IdColor { get; set;}

        public string ColorCode { get; set;}
        public string? CreateUser { get; set;}
        public DateTimeOffset? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTimeOffset? ModifiedDate { get; set; }

        public Flags Flags { get; set;}

        public FlagTypes FlagTypes { get; set;}

        public Colors Colors { get; set;}
    }
}
