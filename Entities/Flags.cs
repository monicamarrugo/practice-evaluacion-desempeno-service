using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Flags", Schema = "Evaluation")]
    public class Flags
    {
        [Key]
        public int IdFlag { get; set;}
        public string NameFlagES { get; set;}
        public string NameFlagEN { get; set; }
        public string? CreateUser { get; set;}
        public DateTimeOffset? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTimeOffset? ModifiedDate { get; set; }

        public ICollection<FlagRules> FlagRules { get; set;}

        [JsonIgnore]
        public ICollection<Evaluations> Evaluations { get; set; }
    }
}
