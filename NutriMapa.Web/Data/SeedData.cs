// Data/SeedData.cs

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace NutriMapa.Web.Data
{
    public static class SeedData
    {
        // Método estático que se ejecuta una sola vez al arrancar la aplicación
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Obtenemos el servicio de manejo de roles de ASP.NET Identity
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Obtenemos el servicio de manejo de usuarios
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Lista de roles que debe existir en el sistema
            string[] roles = { "Administrador", "Donante", "Beneficiario", "Voluntario" };

            // Por cada rol, verificamos si ya existe. Si no, lo creamos.
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Crear un usuario administrador por defecto (para la primera demo)
            string adminEmail = "admin@nutrimapa.do";
            string adminPassword = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var admin = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true // Confirmamos el email automáticamente en la demo
                };

                var result = await userManager.CreateAsync(admin, adminPassword);

                if (result.Succeeded)
                {
                    // Asignamos el rol de Administrador
                    await userManager.AddToRoleAsync(admin, "Administrador");
                }
            }
        }
    }
}
