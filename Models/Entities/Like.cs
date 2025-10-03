using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Like : BaseEntity
    {
        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        // Like có thể cho Post hoặc Comment
        public int? postId { get; set; }
        [ForeignKey(nameof(postId))]
        public Post? post { get; set; }

        public int? commentId { get; set; }
        [ForeignKey(nameof(commentId))]
        public Comment? comment { get; set; }
    }
}
