using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Files", Schema = "Evaluation")]
    public class Files
    {
        [Key]
        public int IdFile { get; set; }
        public int IdEvaluationRecord { get; set; }
        public int Year { get; set; }
        public string NameFile { get; set;}
        public string CdFileType { get; set;}
        public string? CdMonths { get; set;}
        public string? CreateUser { get; set;}
        public DateTimeOffset? CreateDate { get; set;}
        public string? ModifiedUser { get; set;}
        public DateTimeOffset? ModifiedDate { get; set; }
        public FileTypes FileType { get; set; }
    }
}
