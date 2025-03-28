using System;
using TiendaCiclismo.Models; // Asegúrate de incluir el espacio de nombres correcto

namespace TiendaCiclismo.Models
{
    public class Inventario
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public Producto? Producto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal TotalVendido { get; set; }
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }
        public DateTime FechaVenta { get; set; }
    }
}