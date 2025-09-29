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

        [Required, Range(0.01, double.MaxValue)]
        public decimal amount { get; set; }

        [Required, MaxLength(10)]
        public string currency { get; set; } = null!;

        [Required, MaxLength(50)]
        public string paymentMethod { get; set; } = null!;

        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public PaymentStatus status { get; set; }

        [MaxLength(255)]
        public string? transactionId { get; set; }
        public string? description { get; set; }

        [MaxLength(100)]
        public string? orderId { get; set; }

        [MaxLength(255)]
        public string? purchaseToken { get; set; }

        public int? productRefId { get; set; }
        [ForeignKey(nameof(productRefId))]
        public Product? product { get; set; }

        public DateTime? processedAt { get; set; }
    }
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded
    }
}
