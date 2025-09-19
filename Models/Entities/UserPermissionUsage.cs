using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserPermissionUsage : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public int userId { get; set; }
        public User? user { get; set; }

        [ForeignKey(nameof(Permission))]
        public int permissionId { get; set; }
        public Permission? permission { get; set; }

        [Required]
        public int usedCount { get; set; }   // Đã dùng bao nhiêu lần

        [Required]
        public DateTime lastUsedAt { get; set; } = DateTime.UtcNow;
    }
}
