using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class CaresSchedules : BaseEntity
    {
        [Required]
        public int userPlantId { get; set; }

        [ForeignKey(nameof(userPlantId))]
        public UserPlant userPlant { get; set; } = null!;

        [Required, MaxLength(100)]
        public string taskName { get; set; } = null!;

        [Required]
        public DateTime scheduleTime { get; set; }

        [Required]
        public bool isCompleted { get; set; }

        [MaxLength(50)]
        public int frequency { get; set; }     // "Daily", "Weekly", "Monthly"

        public DateTime? nextScheduledDate { get; set; }

        [MaxLength(500)]
        public string? notes { get; set; }

        public DateTime? completedAt { get; set; }
    }

    public enum Frequency
    {
        Daily = 1,
        Weekly = 2,
        Monthly = 3
    }
}
