using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Guide : BaseEntity
    {
        [Required]
        public int plantId { get; set; }

        [ForeignKey(nameof(plantId))]
        public Plant plant { get; set; } = null!;

        [Required, MaxLength(200)]
        public string title { get; set; } = null!;

        [MaxLength(5000)]
        public string? content { get; set; }
    }
}
