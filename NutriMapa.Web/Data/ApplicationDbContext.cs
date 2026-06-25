// Data/ApplicationDbContext.cs

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriMapa.Web.Models;

namespace NutriMapa.Web.Data
{
    // IdentityDbContext ya incluye las tablas de usuarios y roles de ASP.NET Identity
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Declaramos cada tabla como un DbSet (conjunto de registros)
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<FoodDonation> FoodDonations { get; set; }
        //public DbSet<FoodRequest> FoodRequests { get; set; }
        //public DbSet<DeliveryRoute> DeliveryRoutes { get; set; }
        //public DbSet<DonationConfirmation> DonationConfirmations { get; set; }
    }
}
