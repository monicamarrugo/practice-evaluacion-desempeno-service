

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvaluacionDesempenoApi.Models.Entities
{
    [Table("UsersProfiles", Schema = "Security")]
    public class UsersProfiles
    {
        [Key]
        public int IdUsersProfiles { get; set; }

        public int IdUser { get; set; }
        public ApplicationUser User { get; set; }  // Relación con IdentityUser

        public string CdProfile { get; set; }
        public Profiles Profiles { get; set; }  // Asumo que tienes una entidad Profile
    }

}
