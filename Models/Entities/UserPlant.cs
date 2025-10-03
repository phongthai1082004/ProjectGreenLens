using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserPlant : BaseEntity
    {
        [Required]
        public int userId { get; set; }

        [ForeignKey(nameof(userId))]
        public User user { get; set; } = null!;

        [Required]
        public int plantId { get; set; }

        [ForeignKey(nameof(plantId))]
        public Plant plant { get; set; } = null!;

        [MaxLength(100)]
        public string? nickname { get; set; }

        [Required]
        public DateTime acquiredDate { get; set; }

        [MaxLength(2000)]
        public string? notes { get; set; }

        // Thêm trạng thái sức khỏe
        [Required]
        public PlantHealthStatus healthStatus { get; set; } = PlantHealthStatus.Healthy;  // "Healthy", "Sick", "Recovering"

        [MaxLength(500)]
        public string? currentLocation { get; set; }  // Vị trí hiện tại của cây

        public bool isActive { get; set; } = true;    // Còn chăm sóc hay không

        public List<AIAdvicesLogs> aiAdvicesLogs { get; set; } = new();
        public List<CaresSchedules> careSchedules { get; set; } = new();
        public List<CareHistory> careHistories { get; set; } = new();
        public List<UserPlantDisease> userPlantDiseases { get; set; } = new();

    }

    public enum PlantHealthStatus
    {
        Healthy = 0,
        Sick = 1,
        Recovering = 2,
        Unknown = 3,
    }
}
