using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public ProductosController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        // GET: Productos (Acceso público)
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Productos
                .Include(p => p.Proveedor) // Incluir Proveedor
                .ToListAsync();
            return View(productos);
        }

        // GET: Productos/Details/5 (Acceso público)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Proveedor) // Incluir Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // GET: Productos/Create (Protegido)
        [Authorize(Roles = "Admin,Vendedor")]
        public IActionResult Create()
        {
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre");
            return View();
        }

        // POST: Productos/Create (Protegido)
        [HttpPost]
        [Authorize(Roles = "Admin,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Precio,Stock,Categoria,ImagenUrl,ProveedorId")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productos/Edit/5 (Protegido)
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // POST: Productos/Edit/5 (Protegido)
        [HttpPost]
        [Authorize(Roles = "Admin,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Precio,Stock,Categoria,ImagenUrl,ProveedorId")] Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProveedorId"] = new SelectList(_context.Proveedores, "Id", "Nombre", producto.ProveedorId);
            return View(producto);
        }

        // GET: Productos/Delete/5 (Protegido)
        [Authorize(Roles = "Admin,Vendedor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var producto = await _context.Productos
                .Include(p => p.Proveedor) // Incluir Proveedor
                .FirstOrDefaultAsync(m => m.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // POST: Productos/Delete/5 (Protegido)
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin,Vendedor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
