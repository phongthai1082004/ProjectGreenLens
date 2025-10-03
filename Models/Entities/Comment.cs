using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public int postId { get; set; }
        [ForeignKey(nameof(postId))]
        public Post post { get; set; } = null!;

        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        public string content { get; set; } = null!;

        public bool isDeleted { get; set; } = false;

        public List<Like> likes { get; set; } = new();
    }
}
