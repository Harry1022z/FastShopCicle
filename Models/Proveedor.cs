using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string? Contacto { get; set; }
        public string? Direccion { get; set; }

        // Relación con productos (si un proveedor tiene productos, no se podrá eliminar sin manejar esto)
        public ICollection<Producto>? Productos { get; set; }
    }
}