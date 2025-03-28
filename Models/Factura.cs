using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaCiclismo.Models
{
    public class Factura
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; } 

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        public int VendedorId { get; set; }

        [Required]
        public int ProductoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public int Cantidad { get; set; }

        public Vendedor? Vendedor { get; set; }
        public Producto? Producto { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = "Sin descripción.";

    }
}
