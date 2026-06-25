// Models/Domain/UserProfile.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriMapa.Web.Models
{
    public class UserProfile
    {
        // Key indica que esta es la clave primaria
        [Key]
        // ForeignKey indica que UserId apunta al Id de AspNetUsers
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProfileType { get; set; } // 'Donante', 'Beneficiario', 'Voluntario', 'Administrador'

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(200)]
        public string Organization { get; set; }

        public bool IsVerified { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
