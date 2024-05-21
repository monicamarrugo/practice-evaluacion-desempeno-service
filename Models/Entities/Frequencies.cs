using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Frequencies", Schema = "Masters")]
    public class Frequencies
    {
        public string Idfrequency { get; set;}
        [Key]
        public string Cdfrequency { get; set;}
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<QuestionariesConfig> QuestionariesConfig { get; set; }
    }
}
