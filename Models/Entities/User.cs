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
        public List<LogEntry> logEntries { get; set; } = new();
        public List<Payment> payments { get; set; } = new();
        public NurseryProfile? nurseryProfile { get; set; }
        public List<UserPlant> userPlants { get; set; } = new();

        // InverseProperty cho tin nhắn
        [InverseProperty(nameof(UserMessage.sender))]
        public List<UserMessage> sentMessages { get; set; } = new();

        [InverseProperty(nameof(UserMessage.receiver))]
        public List<UserMessage> receivedMessages { get; set; } = new();

        public List<UserToken> userTokens { get; set; } = new();
        public List<AIAdvicesLogs> aiAdvicesLogs { get; set; } = new();
        public ICollection<UserPermissionUsage> permissionUsages { get; set; } = new List<UserPermissionUsage>();

    }
}