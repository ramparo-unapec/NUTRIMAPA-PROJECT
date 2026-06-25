// Controllers/DashboardController.cs

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriMapa.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace NutriMapa.Web.Controllers
{
    // Este controlador requiere que el usuario haya iniciado sesión
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: /Dashboard
        // Detecta el rol del usuario y redirige al dashboard correcto
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            // Redirigimos según el primer rol que tenga el usuario
            if (roles.Contains("Administrador"))
                return RedirectToAction(nameof(Administrador));
            if (roles.Contains("Donante"))
                return RedirectToAction(nameof(Donante));
            if (roles.Contains("Voluntario"))
                return RedirectToAction(nameof(Voluntario));
            if (roles.Contains("Beneficiario"))
                return RedirectToAction(nameof(Beneficiario));

            // Si no tiene rol asignado, cerramos sesión y redirigimos al login
            return RedirectToPage("/Account/Logout", new { area = "Identity" });
        }

        // GET: /Dashboard/Donante — solo accesible para el rol Donante
        [Authorize(Roles = "Donante")]
        public async Task<IActionResult> Donante()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.UserProfiles.FindAsync(user.Id);
            ViewBag.Profile = profile;
            return View();
        }

        // GET: /Dashboard/Voluntario — solo accesible para el rol Voluntario
        [Authorize(Roles = "Voluntario")]
        public async Task<IActionResult> Voluntario()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.UserProfiles.FindAsync(user.Id);
            ViewBag.Profile = profile;
            return View();
        }

        // GET: /Dashboard/Beneficiario — solo accesible para el rol Beneficiario
        [Authorize(Roles = "Beneficiario")]
        public async Task<IActionResult> Beneficiario()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _context.UserProfiles.FindAsync(user.Id);
            ViewBag.Profile = profile;
            return View();
        }

        // GET: /Dashboard/Administrador — solo accesible para el rol Administrador
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Administrador()
        {
            // El admin ve todos los usuarios registrados
            var perfiles = await _context.UserProfiles.ToListAsync();
            return View(perfiles);
        }
    }
}
