using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public required string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
