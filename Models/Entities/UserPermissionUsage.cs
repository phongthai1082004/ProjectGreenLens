using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserPermissionUsage : BaseEntity
    {
        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        public int permissionId { get; set; }
        [ForeignKey(nameof(permissionId))]
        public Permission permission { get; set; } = null!;

        [Required]
        public int usedCount { get; set; }   // Số lần đã dùng quyền này

        [Required]
        public DateTime lastUsedAt { get; set; } = DateTime.UtcNow;
    }
}
