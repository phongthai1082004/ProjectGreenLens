using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class NurseryProfile : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required, MaxLength(100)]
        public string nurseryName { get; set; } = null!;

        [MaxLength(200)]
        public string? address { get; set; }

        [Phone]
        public string? contactNumber { get; set; }

        [MaxLength(1000)]
        public string? description { get; set; }
    }
}
