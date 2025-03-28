using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public required string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Precio { get; set; }

        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public required string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public required string Categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El stock es obligatorio")]
        public int Stock { get; set; }

        public string ImagenUrl { get; set; } = string.Empty;

        // Nuevo: Relación con Proveedor
        public int? ProveedorId { get; set; } // Opcional para evitar errores si no todos los productos tienen proveedor
        public Proveedor? Proveedor { get; set; }

        // Relación inversa con Factura
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();

        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}