using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Notification : BaseEntity
    {
        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        public string type { get; set; } = null!; // e.g. "care_reminder", "system", "comment"
        [Required]
        public string content { get; set; } = null!;
        public bool isRead { get; set; } = false;
    }
}
