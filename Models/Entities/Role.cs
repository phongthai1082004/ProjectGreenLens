using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string name { get; set; } = null!;

        [MaxLength(500)]
        public string description { get; set; } = null!;

        public ICollection<RolePermission> rolePermissions { get; set; } = new List<RolePermission>();
        public ICollection<User> users { get; set; } = new List<User>();
    }
}
