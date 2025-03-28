using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public required string Correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        public required string Telefono { get; set; }

        // Relación inversa con Factura
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        // Relación inversa con Inventario
        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}
