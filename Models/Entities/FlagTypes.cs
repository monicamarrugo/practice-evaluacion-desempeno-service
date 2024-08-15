using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("FlagTypes", Schema = "Masters")]
    public class FlagTypes
    {
        public int IdFlagType { get; set;}
        [Key]
        public string CdFlagType { get; set;}
        public string FlagTypeNameES { get; set;}
        public string FlagTypeNameEN { get; set; }
        [JsonIgnore]
        public ICollection<FlagRules> FlagRules { get; set; }
    }
}
