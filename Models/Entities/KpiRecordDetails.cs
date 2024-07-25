using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Azure.Core.HttpHeader;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("KpiRecordDetails", Schema = "Evaluation")]
    public class KpiRecordDetails
    {
        [Key]
        public int IdKpiRecordDetails { get; set;}
        public int IdEvaluationRecord { get; set;}
        public int? Weight { get; set;}
        public string? Control { get; set;}
        public string? DisplayFormats { get; set;}

        public string? CdFormats { get; set;}
        public string? Cdfrequency { get; set;}
        public decimal? Incentive { get; set;}
        public string? NameES { get; set;}
        public string? NameEN { get; set;}
        public string CdMonths { get; set;}
        public string RealValue { get; set;}
        public bool Won { get; set;}
        public decimal Earned { get; set;}
        public string?  KpiFile { get; set;}
        public string? FileType { get; set; }

        public EvaluationRecord EvaluationRecord { get; set; }
    }
}
