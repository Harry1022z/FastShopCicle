using Microsoft.AspNetCore.Mvc.Rendering;

namespace TiendaCiclismo.Models
{
    public class AsignarRolViewModel
    {
        public required Usuario Usuario { get; set; }
        public int RolId { get; set; }
        public required IEnumerable<SelectListItem> Roles { get; set; }
    }
}
