using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("FileTypes", Schema = "Masters")]
    public class FileTypes
    {
        public string IdFileType { get; set;}

        [Key]
        public string CdFileType { get; set;}
        public string NameFileTypeES { get; set;}
        public string NameFileTypeEN { get; set; }

        [JsonIgnore]
        public ICollection<Files> Files { get; set; }
    }
}
