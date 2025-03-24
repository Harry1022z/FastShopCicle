using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class Vendedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public required string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public required string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        public required string Telefono { get; set; } = string.Empty;

        // Relación inversa con Factura
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
