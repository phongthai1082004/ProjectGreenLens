using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(50)]
        public string username { get; set; } = null!;

        [Required, EmailAddress, MaxLength(100)]
        public string email { get; set; } = null!;

        [Required]
        public string passwordHash { get; set; } = null!;

        [Required]
        [Column("roleId")]
        public int roleId { get; set; }

        [ForeignKey(nameof(roleId))]
        public Role role { get; set; } = null!;
        public bool isEmailVerified { get; set; } = false;

        // Navigation properties
        public UserProfile? userProfile { get; set; }
        public List<Payment> payments { get; set; } = new();
        public List<UserPlant> userPlants { get; set; } = new();
        public List<UserToken> userTokens { get; set; } = new();
        public List<AIAdvicesLogs> aiAdvicesLogs { get; set; } = new();
        public List<UserPermissionUsage> permissionUsages { get; set; } = new();
        public List<Post> posts { get; set; } = new();
        public List<Comment> comments { get; set; } = new();
        public List<Like> likes { get; set; } = new();
        public List<Notification> notifications { get; set; } = new();
        public List<ContactMessage> contactMessages { get; set; } = new();
        public List<SavedPlant> savedPlants { get; set; } = new();
    }
}