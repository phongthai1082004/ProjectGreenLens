using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserProfile : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [MaxLength(100)]
        public string? fullName { get; set; }

        [Url]
        public string? avatarUrl { get; set; }

        [MaxLength(1000)]
        public string? bio { get; set; }

        [MaxLength(100)]
        public string? location { get; set; }
    }
}
