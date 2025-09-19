using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class PermissionQuota : BaseEntity
    {
        [ForeignKey(nameof(RolePermission))]
        public int roleId { get; set; }
        public Role? role { get; set; }

        [ForeignKey(nameof(RolePermission))]
        public int permissionId { get; set; }
        public Permission? permission { get; set; }

        [Required]
        public int usageLimit { get; set; }
    }
}
