// Models/Domain/FoodDonation.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace NutriMapa.Web.Models
{
    public class FoodDonation
    {
        [Key]
        public int DonationId { get; set; }

        [Required]
        public string DonorUserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FoodType { get; set; }

        [Required]
        public decimal QuantityKg { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [MaxLength(200)]
        public string StorageConditions { get; set; }

        [Required]
        [MaxLength(300)]
        public string PickupAddress { get; set; }

        public TimeSpan PickupStartTime { get; set; }
        public TimeSpan PickupEndTime { get; set; }

        // Estado del ciclo de vida de la donación
        [MaxLength(50)]
        public string Status { get; set; } = "Disponible";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
