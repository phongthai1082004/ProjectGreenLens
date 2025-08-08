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

        [Required, MaxLength(20)]
        public string status { get; set; } = null!;

        [MaxLength(100)]
        public string? transactionId { get; set; }

        [MaxLength(500)]
        public string? description { get; set; }

        public DateTime? processedAt { get; set; }
    }

}
