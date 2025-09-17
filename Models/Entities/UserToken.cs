using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserToken : BaseEntity
    {
        [Required]
        public string token { get; set; } = null!;
        [Required]
        public DateTime expiresAt { get; set; }
        [Required]
        public bool isRevoked { get; set; } = false;
        [Required]
        public TokenType type { get; set; }
        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        public enum TokenType
        {
            AccessToken = 0,
            RefreshToken = 1,
            ResetPassword = 2,
            VerifyEmail = 3
        }
    }
}