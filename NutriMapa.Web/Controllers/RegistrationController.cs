// Controllers/RegistrationController.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NutriMapa.Web.Data;
using NutriMapa.Web.Models;
using NutriMapa.Web.Models.ViewModels;
using System.Threading.Tasks;

namespace NutriMapa.Web.Controllers
{
    public class RegistrationController : Controller
    {
        // Inyección de dependencias: ASP.NET Core nos provee estos servicios automáticamente
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public RegistrationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: /Registration/SelectProfile
        // Muestra la pantalla de selección de perfil (Pantalla 3 del Figma)
        [HttpGet]
        public IActionResult SelectProfile()
        {
            return View();
        }

        // GET: /Registration/Register?profileType=Donante
        // Muestra el formulario de registro según el perfil seleccionado
        [HttpGet]
        public IActionResult Register(string profileType)
        {
            // Validamos que el perfil recibido sea uno de los cuatro válidos
            var validProfiles = new[] { "Donante", "Beneficiario", "Voluntario" };
            if (!System.Array.Exists(validProfiles, p => p == profileType))
            {
                return RedirectToAction(nameof(SelectProfile));
            }

            // Pasamos el tipo de perfil a la vista para que adapte el formulario
            ViewBag.ProfileType = profileType;
            return View();
        }

        // POST: /Registration/Register
        // Procesa el formulario de registro
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección contra ataques CSRF
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Si hay errores de validación, regresamos el formulario con los mensajes de error
                ViewBag.ProfileType = model.ProfileType;
                return View(model);
            }

            // Creamos el objeto de usuario de Identity
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            // Intentamos crear el usuario con su contraseña
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Asignamos el rol seleccionado
                await _userManager.AddToRoleAsync(user, model.ProfileType);

                // Creamos el perfil extendido en nuestra tabla UserProfiles
                var profile = new UserProfile
                {
                    UserId = user.Id,
                    FullName = model.FullName,
                    ProfileType = model.ProfileType,
                    PhoneNumber = model.PhoneNumber,
                    Organization = model.Organization,
                    Address = model.Address
                };

                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();

                // Iniciamos sesión automáticamente después del registro
                await _signInManager.SignInAsync(user, isPersistent: false);

                // Redirigimos al dashboard correspondiente según el rol
                return RedirectToAction(model.ProfileType, "Dashboard");
            }

            // Si la creación falló (por ejemplo, email ya existente), mostramos los errores
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            ViewBag.ProfileType = model.ProfileType;
            return View(model);
        }
    }
}
