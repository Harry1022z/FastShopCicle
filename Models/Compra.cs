using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaCiclismo.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; } // ID del usuario que realiza la compra

        [Required(ErrorMessage = "Debe seleccionar un vendedor.")]
        public int VendedorId { get; set; } // ID del vendedor

        [ForeignKey("VendedorId")]
        public Vendedor? Vendedor { get; set; } // Relación con la tabla Vendedores

        [Required(ErrorMessage = "Debe agregar al menos un producto.")]
        public List<Producto> Productos { get; set; } = new List<Producto>();

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0.")]
        public decimal Total { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
