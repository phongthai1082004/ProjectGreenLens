using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class AIAdvicesLogs : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        public int? userPlantId { get; set; }

        [ForeignKey(nameof(userPlantId))]
        public UserPlant? userPlant { get; set; }
        public int? plantId { get; set; }
        [ForeignKey(nameof(plantId))]
        public Plant? plant { get; set; }

        [Required, MaxLength(20)]
        public string role { get; set; } = "user";

        [Required, MaxLength(4000)]
        public string content { get; set; } = null!;

        public string? conversationType { get; set; } // owned, store, following, unknown
    }
}
