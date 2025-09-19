using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Entities
{
    public class Permission : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public required string name { get; set; }

        [MaxLength(500)]
        public required string description { get; set; }

        public ICollection<RolePermission> rolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<UserPermissionUsage> userPermissionUsages { get; set; } = new List<UserPermissionUsage>();
    }
}
