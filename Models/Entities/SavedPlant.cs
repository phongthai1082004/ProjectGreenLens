using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class SavedPlant : BaseEntity
    {
        [Required]
        public int userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        public int plantId { get; set; }
        [ForeignKey(nameof(plantId))]
        public Plant plant { get; set; } = null!;

        public string? affiliateUrl { get; set; }
    }
}
