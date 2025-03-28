using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        // LISTAR TODAS LAS FACTURAS
        public async Task<IActionResult> Index()
        {
            var facturas = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .ToListAsync();

            return View(facturas);
        }

        // CREAR FACTURA - FORMULARIO
        public IActionResult Create()
        {
            var productos = _context.Productos.ToList();
            var vendedores = _context.Vendedores.ToList();

            // Validar que haya productos y vendedores
            if (productos == null || vendedores == null || !productos.Any() || !vendedores.Any())
            {
                TempData["Error"] = "Debe haber al menos un producto y un vendedor para registrar una factura.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Productos = productos ?? new List<Producto>();
            ViewBag.Vendedores = vendedores ?? new List<Vendedor>();

            return View();
        }

        // CREAR FACTURA - PROCESO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura factura)
        {
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                TempData["Error"] = "Errores en el formulario: " + string.Join(", ", errores);
                ViewBag.Productos = _context.Productos.ToList();
                ViewBag.Vendedores = _context.Vendedores.ToList();
                return View(factura);
            }

            factura.Fecha = DateTime.Now;

            var producto = await _context.Productos.FindAsync(factura.ProductoId);
            var vendedor = await _context.Vendedores.FindAsync(factura.VendedorId);

            if (producto == null || vendedor == null)
            {
                ModelState.AddModelError("", "Producto o vendedor no encontrados.");
                ViewBag.Productos = _context.Productos.ToList();
                ViewBag.Vendedores = _context.Vendedores.ToList();
                return View(factura);
            }

            if (factura.Cantidad <= 0)
            {
                ModelState.AddModelError("", "La cantidad debe ser mayor que 0.");
                return View(factura);
            }

            if (producto.Stock < factura.Cantidad)
            {
                ModelState.AddModelError("", "Stock insuficiente para este producto.");
                return View(factura);
            }

            // Reducir stock y calcular total
            producto.Stock -= factura.Cantidad;
            factura.Total = producto.Precio * factura.Cantidad;

            try
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Factura registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "Error de base de datos: " + dbEx.InnerException?.Message);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            }

            ViewBag.Productos = _context.Productos.ToList();
            ViewBag.Vendedores = _context.Vendedores.ToList();
            return View(factura);
        }

        // VER DETALLE DE FACTURA
        public async Task<IActionResult> Details(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
            {
                TempData["Error"] = "Factura no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(factura);
        }

        // ELIMINAR FACTURA - CONFIRMACIÓN
        public async Task<IActionResult> Delete(int id)
        {
            var factura = await _context.Facturas
                .Include(f => f.Vendedor)
                .Include(f => f.Producto)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (factura == null)
            {
                TempData["Error"] = "Factura no encontrada.";
                return RedirectToAction(nameof(Index));
            }

            return View(factura);
        }

        // ELIMINAR FACTURA - PROCESO
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var factura = await _context.Facturas.FindAsync(id);
                if (factura == null)
                {
                    TempData["Error"] = "Factura no encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Factura eliminada correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "No se puede eliminar la factura debido a dependencias en la base de datos.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error inesperado: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
