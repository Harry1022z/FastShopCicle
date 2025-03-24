using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(100, ErrorMessage = "El nombre completo no puede tener más de 100 caracteres")]
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
