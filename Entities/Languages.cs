using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Languages", Schema = "Masters")]
    public class Languages
    {
        public int IdLanguage { get; set;}
        [Key]
        public string CdLanguage { get; set;}
        public string LanguageName { get; set; }

        [JsonIgnore]
        public ICollection<Users> Users { get; set; }
    }
}
