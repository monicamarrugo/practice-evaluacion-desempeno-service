using Microsoft.AspNetCore.Identity;

namespace EvaluacionDesempenoApi.Models.Entities
{
    public class ApplicationUser : IdentityUser<int>  // Usa 'int' como el tipo de la clave primaria
    {
        public int? IdEmployee { get; set; }
        public bool IndEnabled { get; set; }
        public string? AltName { get; set; }
        public string? AltEmail { get; set; }
        public string CdLanguage { get; set; }
        public bool IndChangePassword { get; set; }

        // Relación uno a uno con Employee
        public Employees? Employees { get; set; }

        // Relación uno a muchos con UsersProfiles
        public ICollection<UsersProfiles> UsersProfiles { get; set; }
    }
}
