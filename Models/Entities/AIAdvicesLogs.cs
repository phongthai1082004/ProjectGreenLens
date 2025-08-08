using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class AIAdvicesLogs : BaseEntity
    {
        [Required]
        public int userPlantId { get; set; }

        [ForeignKey(nameof(userPlantId))]
        public UserPlant userPlant { get; set; } = null!;

        [MaxLength(2000)]
        public string? adviceText { get; set; }
    }
}
