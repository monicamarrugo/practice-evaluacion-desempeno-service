using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Colors", Schema = "Masters")]
    public class Colors
    {
        [Key]
        public int IdColor { get; set;}
        public string ColorCode { get; set;}
        public string ColorNameES { get; set; }
        public string ColorNameEN { get; set; }
        [JsonIgnore]
        public ICollection<FlagRules> FlagRules { get; set; }
    }
}
