using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Controllers
{
    [Authorize(Roles = "Admin,Vendedor")]
    public class FacturasController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public FacturasController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .ToListAsync();
            return View(facturas);
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var factura = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (factura == null) return NotFound();

            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Vendedores = _context.Vendedores.ToList();
            return View();
        }

        // POST: Facturas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura factura)
        {
            if (ModelState.IsValid)
            {
                factura.Fecha = DateTime.Now;

                var producto = await _context.Productos.FindAsync(factura.ProductoId);
                if (producto == null)
                {
                    ModelState.AddModelError("", "Producto no encontrado.");
                    ViewBag.Productos = _context.Productos.ToList();
                    ViewBag.Vendedores = _context.Vendedores.ToList();
                    return View(factura);
                }

                factura.Total = producto.Precio * factura.Cantidad;

                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Vendedores = _context.Vendedores.ToList();
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var factura = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (factura == null) return NotFound();

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
