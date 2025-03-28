using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;
using System.Linq;

namespace TiendaCiclismo.Controllers
{
    public class CompraController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public CompraController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Vendedores = new SelectList(_context.Vendedores, "Id", "Nombre"); // Lista de vendedores
            return View(new Compra());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Compra compra)
        {
            if (!compra.Productos.Any())
            {
                ModelState.AddModelError("Productos", "Debe agregar al menos un producto.");
            }

            if (ModelState.IsValid)
            {
                // Verificar que el vendedor existe
                var vendedorExiste = _context.Vendedores.Any(v => v.Id == compra.VendedorId);
                if (!vendedorExiste)
                {
                    ModelState.AddModelError("VendedorId", "El vendedor seleccionado no existe.");
                    ViewBag.Productos = _context.Productos.ToList();
                    ViewBag.Vendedores = new SelectList(_context.Vendedores, "Id", "Nombre");
                    return View(compra);
                }

                _context.Compras.Add(compra);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Vendedores = new SelectList(_context.Vendedores, "Id", "Nombre");
            return View(compra);
        }
    }
}
