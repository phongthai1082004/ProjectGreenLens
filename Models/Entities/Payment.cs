using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Payment : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal amount { get; set; }

        [Required, MaxLength(10)]
        public string currency { get; set; } = "VND";

        [Required, MaxLength(20)]
        public string status { get; set; } = "Pending"; // Pending, Success, Failed

        [Required, MaxLength(50)]
        public string paymentMethod { get; set; } = "VNPay";

        [MaxLength(255)]
        public string? transactionId { get; set; }

        [MaxLength(100)]
        public string? orderId { get; set; }

        public DateTime? processedAt { get; set; }
    }
}
