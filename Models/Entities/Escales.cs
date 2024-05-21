using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Escales", Schema = "Masters")]
    public class Escales
    {
        [Key]
        public int IdEscales { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public ICollection<EscalesValues> EscalesValues { get; set; }

        [JsonIgnore]
        public ICollection<Evaluations> Evaluations { get; set; }

    }
}
