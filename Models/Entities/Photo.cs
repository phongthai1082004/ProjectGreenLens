using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Photo : BaseEntity
    {
        [Required]
        public int plantId { get; set; }

        [ForeignKey(nameof(plantId))]
        public Plant plant { get; set; } = null!;

        [Required, Url]
        public string photoUrl { get; set; } = null!;

        [MaxLength(250)]
        public string? caption { get; set; }
    }
}
