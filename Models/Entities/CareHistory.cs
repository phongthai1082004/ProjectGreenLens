using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class CareHistory : BaseEntity
    {
        [Required]
        public int userPlantId { get; set; }

        [ForeignKey(nameof(userPlantId))]
        public UserPlant userPlant { get; set; } = null!;

        [Required, MaxLength(50)]
        public int careType { get; set; } = 1;  // "Watering", "Fertilizing", "Pruning", "Repotting", "Pest_Control"

        [Required]
        public DateTime careDate { get; set; }

        [MaxLength(500)]
        public string? notes { get; set; }  //

        [MaxLength(100)]
        public string? quantity { get; set; }

        [Url]
        public string? photoUrl { get; set; }

        [Range(1, 5)]
        public int? effectiveness { get; set; }
    }
}
