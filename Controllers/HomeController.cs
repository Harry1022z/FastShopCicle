using Microsoft.AspNetCore.Mvc;

namespace TiendaCiclismo.Controllers
{
    public class HomeController : Controller
    {
        // Página principal
        public IActionResult Index()
        {
            return View();
        }

        // Página de privacidad (puedes personalizarla después)
        public IActionResult Privacy()
        {
            return View();
        }

        // Página de error (opcionalmente ya la maneja el pipeline)
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
