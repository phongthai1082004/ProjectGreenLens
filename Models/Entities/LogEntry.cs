using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class LogEntry : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required, MaxLength(100)]
        public string action { get; set; } = null!;

        public string? metadata { get; set; }
    }
}
