using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("EscalesValues", Schema = "Masters")]
    public class EscalesValues
    {
        [Key]
        public int IdEscalesValues { get; set; }
        public int IdEscales { get; set; }
        public Escales Escales { get; set; }
        public string Value { get; set; }
        public string NameValue { get; set; }
        public int IndOrder { get; set; }
        public bool IndMayor { get; set; }
    }
}
