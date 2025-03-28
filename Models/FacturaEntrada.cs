using System;
using System.Collections.Generic;

namespace TiendaCiclismo.Models
{
    public class FacturaEntrada
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int ProveedorId { get; set; } // Clave foránea
        public Proveedor? Proveedor { get; set; } // Propiedad de navegación
        public decimal Total { get; set; }
    }
}