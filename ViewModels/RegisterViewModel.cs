using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100)]
        public required string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public required string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public required string ConfirmPassword { get; set; } = string.Empty;
    }
}
