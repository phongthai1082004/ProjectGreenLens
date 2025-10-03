using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserPlantDisease : BaseEntity
    {
        [Required]
        public int userPlantId { get; set; }

        [ForeignKey(nameof(userPlantId))]
        public UserPlant userPlant { get; set; } = null!;

        [Required]
        public int diseaseId { get; set; }
        [ForeignKey(nameof(diseaseId))]
        public Disease disease { get; set; } = null!;

        [Required]
        public DateTime detectedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(50)]
        public string status { get; set; } = "Suspected"; // ví dụ: Suspected / Confirmed / Treated

        [MaxLength(2000)]
        public string? notes { get; set; }
    }
}
