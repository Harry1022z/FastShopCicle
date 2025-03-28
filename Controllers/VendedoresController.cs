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
    [Authorize(Roles = "Admin")]
    public class VendedoresController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public VendedoresController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vendedores = await _context.Vendedores.ToListAsync();
            return View(vendedores);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores
                .Include(v => v.Facturas)
                .Include(v => v.Inventarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores.FindAsync(id);
            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vendedor = await _context.Vendedores
                .Include(v => v.Facturas)
                .Include(v => v.Inventarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (vendedor == null) return NotFound();

            return View(vendedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendedor = await _context.Vendedores
                .Include(v => v.Facturas)
                .Include(v => v.Inventarios)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vendedor == null) return NotFound();

            try
            {
                // Eliminar las facturas asociadas
                _context.Facturas.RemoveRange(vendedor.Facturas);
                
                // Eliminar los inventarios asociados
                _context.Inventarios.RemoveRange(vendedor.Inventarios);

                // Eliminar el vendedor
                _context.Vendedores.Remove(vendedor);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar el vendedor: {ex.Message}");
                return View(vendedor);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedores.Any(e => e.Id == id);
        }
    }
}
