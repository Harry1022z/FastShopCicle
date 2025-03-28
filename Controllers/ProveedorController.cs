using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Controllers
{
    [Authorize]
    public class ProveedorController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public ProveedorController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var proveedores = _context.Proveedores.ToList();
            return View(proveedores);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Proveedores.Add(proveedor);
                _context.SaveChanges();
                TempData["Success"] = "Proveedor creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public IActionResult Edit(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Proveedor proveedor)
        {
            if (id != proveedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(proveedor);
                _context.SaveChanges();
                TempData["Success"] = "Proveedor actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            return View(proveedor);
        }

        public IActionResult Delete(int id)
        {
            var proveedor = _context.Proveedores.Find(id);
            if (proveedor == null) return NotFound();
            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var proveedor = _context.Proveedores
                .Include(p => p.Productos) // Incluir productos relacionados
                .FirstOrDefault(p => p.Id == id);

            if (proveedor == null)
            {
                TempData["Error"] = "El proveedor no fue encontrado.";
                return RedirectToAction(nameof(Index));
            }

            // Validar que no tenga productos antes de eliminarlo
            if (proveedor.Productos != null && proveedor.Productos.Any())
            {
                TempData["Error"] = "No se puede eliminar el proveedor porque tiene productos asociados.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Proveedores.Remove(proveedor);
                _context.SaveChanges();
                TempData["Success"] = "Proveedor eliminado correctamente.";
            }
            catch (DbUpdateException)
            {
                TempData["Error"] = "Error al eliminar. Asegúrese de que no haya registros dependientes.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
