using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TiendaCiclismo.ViewModels;

namespace TiendaCiclismo.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Roles/Index
        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/AssignRole
        public Task<IActionResult> AssignRole()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            var model = new AssignRoleViewModel
            {
                Users = users,
                Roles = roles.Where(r => r != null).Cast<string>().ToList()
            };

            return Task.FromResult<IActionResult>(View(model));
        }

        // POST: Roles/AssignRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(roleName))
            {
                TempData["Error"] = "Usuario o rol inválido.";
                return RedirectToAction("AssignRole");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                TempData["Success"] = "Rol asignado correctamente.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Error"] = "Error al asignar rol.";
                return RedirectToAction("AssignRole");
            }
        }
    }
}
