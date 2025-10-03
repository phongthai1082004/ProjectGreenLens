using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        public int authorId { get; set; }
        [ForeignKey(nameof(authorId))]
        public User author { get; set; } = null!;

        [Required, MaxLength(200)]
        public string title { get; set; } = null!;

        [Required]
        public string content { get; set; } = null!;

        public bool isPublished { get; set; } = true;
        public bool isHidden { get; set; } = false;

        public List<Comment> comments { get; set; } = new();
        public List<Like> likes { get; set; } = new();

    }
}
