using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Entities
{
    [Table("Groups", Schema = "Masters")]
    public class Groups
    {
        [Key]
        public int IdGroups { get; set; }
        public string NameES { get; set; }
        public string NameEN { get; set; }
        public string? DescriptionES { get; set; }
        public string? DescriptionEN { get; set; }
        public bool Enabled { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }

        [JsonIgnore]
        public ICollection<QuestionGroupRelation> QuestionGroupRelations { get; set; }
    }
}
