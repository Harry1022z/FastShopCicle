using Microsoft.AspNetCore.Mvc;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TiendaCiclismo.Controllers
{
    public class InventarioController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public InventarioController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var inventario = _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Vendedor)
                .ToList();
            return View(inventario);
        }
    }
}