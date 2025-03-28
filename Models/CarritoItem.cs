using System.ComponentModel.DataAnnotations;

namespace TiendaCiclismo.Models
{
    public class CarritoItem
    {
        public int ProductoId { get; set; }

        // 🔹 Aseguramos que Producto nunca sea null
        [Required]
        public required Producto Producto { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1")]
        public int Cantidad { get; set; }
    }
}
