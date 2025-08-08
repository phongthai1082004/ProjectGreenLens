using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class ArModel : BaseEntity
    {
        [Required]
        public int plantId { get; set; }

        [ForeignKey(nameof(plantId))]
        public Plant plant { get; set; } = null!;

        [Required, Url]
        public string modelUrl { get; set; } = null!;

        [MaxLength(10)]
        public string? fileFormat { get; set; }
    }
}
