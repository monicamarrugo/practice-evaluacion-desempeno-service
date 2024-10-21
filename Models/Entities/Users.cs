using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EvaluacionDesempenoApi.Models.Entities
{
    public class Users
    {
        [Key]
        public int IdUser { get; set; }
        public int? IdEmployee { get; set; }
        public bool? IndEnabled { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? AltName { get; set; }
        public string? AltEmail { get; set; }
        public string? CdLanguage { get; set; }

        public bool? IndChangePassword { get; set; } 
        public string? CreateUser { get; set; }
        public DateTimeOffset? CreateDate { get; set; }
        public string? ModifiedUser { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }

        
        //public ICollection<UsersProfiles> UsersProfiles { get; set; }

        [ForeignKey("IdEmployee")]  // Indica que 'IdEmployee' es la clave foránea
        public Employees Employees { get; set; }

        public Languages Languages { get; set; }
    }
}
