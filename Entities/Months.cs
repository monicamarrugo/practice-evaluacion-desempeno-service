using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Months", Schema = "Masters")]
    public class Months
    {
        public int IdMonths { get;set;}

        [Key]
        public int CdMonths { get;set;}
        public string NameEs { get;set;}
        public string NameEn { get; set; }
    }
}
