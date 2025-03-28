using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Controllers
{
    public class FacturaEntradaController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public FacturaEntradaController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var facturasEntrada = _context.FacturasEntrada
                .Include(f => f.Proveedor)
                .ToList();
            return View(facturasEntrada);
        }

                public IActionResult Create()
        {
            ViewBag.Proveedores = _context.Proveedores.ToList();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FacturaEntrada facturaEntrada)
        {
            if (ModelState.IsValid)
            {
                _context.FacturasEntrada.Add(facturaEntrada);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Proveedores = _context.Proveedores.ToList();
            return View(facturaEntrada);
        }
    }
}