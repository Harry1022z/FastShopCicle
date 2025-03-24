using Microsoft.AspNetCore.Identity;

namespace TiendaCiclismo.ViewModels
{
    public class AssignRoleViewModel
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public List<string> RolesDisponibles { get; set; } = new List<string>();
        public List<string> RolesAsignados { get; set; } = new List<string>();

        // Agregar estas dos listas:
        public List<IdentityUser> Users { get; set; } = new List<IdentityUser>();
        public List<string> Roles { get; set; } = new List<string>();
    }
}
