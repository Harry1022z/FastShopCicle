using System.Collections.Generic;

namespace TiendaCiclismo.Models
{
    public class ConfirmarCompraViewModel
    {
        public List<Vendedor> Vendedores { get; set; } = new List<Vendedor>(); // ✅ Se inicializa para evitar null
        public int VendedorId { get; set; }
    }
}
