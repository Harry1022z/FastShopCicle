using Microsoft.AspNetCore.Mvc;
using TiendaCiclismo.Data;
using TiendaCiclismo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaCiclismo.Controllers
{
    public class CarritoController : Controller
    {
        private readonly TiendaCiclismoContext _context;

        public CarritoController(TiendaCiclismoContext context)
        {
            _context = context;
        }

        // 🔹 Muestra los productos en el carrito y la lista de vendedores
        public IActionResult Index()
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            
            // Obtener la lista de vendedores disponibles
            ViewBag.Vendedores = _context.Vendedores.ToList();

            return View(carrito);
        }

        // 🔹 Agrega un producto al carrito
        public IActionResult Agregar(int productoId)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            var producto = _context.Productos.Find(productoId);

            if (producto == null)
            {
                ModelState.AddModelError("", "El producto no existe.");
                return RedirectToAction("Index");
            }

            var itemExistente = carrito.FirstOrDefault(c => c.ProductoId == productoId);
            if (itemExistente != null)
            {
                itemExistente.Cantidad++;
            }
            else
            {
                carrito.Add(new CarritoItem { ProductoId = productoId, Producto = producto, Cantidad = 1 });
            }

            HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            return RedirectToAction("Index");
        }

        // 🔹 Elimina un producto del carrito
        public IActionResult Eliminar(int productoId)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();
            var item = carrito.FirstOrDefault(c => c.ProductoId == productoId);

            if (item != null)
            {
                carrito.Remove(item);
                HttpContext.Session.SetObjectAsJson("Carrito", carrito);
            }

            return RedirectToAction("Index");
        }

        // 🔹 Confirma la compra y genera la factura
        [HttpPost]
        public async Task<IActionResult> ConfirmarCompra(int VendedorId)
        {
            var carrito = HttpContext.Session.GetObjectFromJson<List<CarritoItem>>("Carrito") ?? new List<CarritoItem>();

            if (!carrito.Any())
            {
                ModelState.AddModelError("", "El carrito está vacío. Agrega productos antes de confirmar la compra.");
                return RedirectToAction("Index");
            }

            foreach (var item in carrito)
            {
                var producto = await _context.Productos.FindAsync(item.ProductoId);
                if (producto == null || producto.Stock < item.Cantidad)
                {
                    ModelState.AddModelError("", $"No hay suficiente stock para {producto?.Nombre ?? "producto desconocido"}.");
                    return RedirectToAction("Index");
                }

                producto.Stock -= item.Cantidad;
            }

            var descripcion = string.Join(", ", carrito.Select(c => $"{c.Cantidad}x {c.Producto?.Nombre ?? "Producto desconocido"}"));

            var factura = new Factura
            {
                Fecha = DateTime.Now,
                Total = carrito.Sum(c => c.Cantidad * (c.Producto?.Precio ?? 0)),
                Descripcion = descripcion,
                VendedorId = VendedorId,  // 🔹 El vendedor seleccionado por el usuario
                ProductoId = carrito.First().ProductoId,
                Cantidad = carrito.Sum(c => c.Cantidad)
            };

            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Carrito");

            return RedirectToAction("Factura", new { id = factura.Id });
        }

        // 🔹 Muestra la factura generada
        public async Task<IActionResult> Factura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null) return NotFound();
            return View(factura);
        }
    }
}
