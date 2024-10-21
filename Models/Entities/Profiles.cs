using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("Profiles", Schema = "Security")]
    public class Profiles
    {
        public int IdProfile { get; set; }

        [Key]
        public string CdProfile { get; set; }
        public string ProfileNameES { get; set; }
        public string ProfileNameEN { get; set; }
        public string? CreateUser { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

      
        public ICollection<UsersProfiles> UsersProfiles { get; set; }
    }
}
