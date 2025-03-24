using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;

namespace TiendaCiclismo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VendedoresController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public VendedoresController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        // GET: Vendedores
        public async Task<IActionResult> Index()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return View(vendedores);
        }

        // GET: Vendedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

        // GET: Vendedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Correo,Telefono")] Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

        // POST: Vendedores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Correo,Telefono")] Vendedor vendedor)
        {
            if (id != vendedor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vendedor);
        }

        // GET: Vendedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

        // POST: Vendedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor != null)
            {
                _context.Vendedores.Remove(vendedor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Vendedores/FacturasPorVendedor/5
        public async Task<IActionResult> FacturasPorVendedor(int? id)
        {
            if (id == null) return NotFound();

            var facturas = await _context.Facturas
                .Include(f => f.Vendedor)
                .Where(f => f.VendedorId == id)
                .ToListAsync();

            ViewBag.Vendedor = await _context.Vendedores.FindAsync(id);

            return View(facturas);
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.Id == id);
        }
    }
}
