// Models/ViewModels/RegisterViewModel.cs

using System.ComponentModel.DataAnnotations;

namespace NutriMapa.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre completo es requerido")]
        [MaxLength(200)]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmPassword { get; set; }

        [MaxLength(20)]
        [Display(Name = "Teléfono")]
        public string PhoneNumber { get; set; }

        [MaxLength(200)]
        [Display(Name = "Organización / Comercio")]
        public string Organization { get; set; }

        [MaxLength(300)]
        [Display(Name = "Dirección")]
        public string Address { get; set; }

        // Este campo llega como parámetro oculto del formulario
        [Required]
        public string ProfileType { get; set; }
    }
}
