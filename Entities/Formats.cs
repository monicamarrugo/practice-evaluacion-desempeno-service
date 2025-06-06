using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Formats", Schema = "Masters")]
    public class Formats
    {
        [Key]
        public int IdFormats { get; set; }
        public string NameFormats { get; set; }
        public string CdFormats { get; set; }
        public string? DescriptionFormats { get; set; }
        public string? display { get; set; }
        public Questions Questions { get; set; }
    }
}
