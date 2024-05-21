using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Areas", Schema = "Masters")]
    public class Areas
    {
        public int IdArea { get; set;}

        [Key]
        public string CdArea { get; set;}
        public string NameArea { get; set; }

        [JsonIgnore]
        public ICollection<Questions> Questions { get; set; }

        [JsonIgnore]
        public ICollection<Questionaries> Questionaries { get; set; }
    }
}
