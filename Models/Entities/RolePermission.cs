using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class RolePermission
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Role))]
        public int roleId { get; set; }
        [Required]
        public Role? role { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Permission))]
        public int permissionId { get; set; }
        [Required]
        public Permission? permission { get; set; }
    }
}
