using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Divisions", Schema = "Masters")]
    public class Divisions
    {
        
        public int IdDivisions { get; set;}

        [Key]
        public string CdDivisions { get; set;}
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Employees> Employees { get; set; }
    }
}
