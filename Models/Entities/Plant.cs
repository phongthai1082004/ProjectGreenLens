using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class Plant : BaseEntity
    {
        [Required, MaxLength(150)]
        public string scientificName { get; set; } = null!;

        [MaxLength(150)]
        public string? commonName { get; set; }

        [MaxLength(2000)]
        public string? description { get; set; }

        [MaxLength(2000)]
        public string? careInstructions { get; set; }

        // Thêm foreign key cho category
        public int? plantCategoryId { get; set; }

        [ForeignKey(nameof(plantCategoryId))]
        public PlantCategory? plantCategory { get; set; }


        public bool isIndoor { get; set; }         // Cây trong nhà hay ngoài trời

        [MaxLength(100)]
        public int wateringFrequency { get; set; }

        [MaxLength(100)]
        public int lightRequirement { get; set; }

        [MaxLength(100)]
        public string? soilType { get; set; }           // Loại đất phù hợp

        public decimal? averagePrice { get; set; }      // Giá trung bình

        public List<Photo> photos { get; set; } = new();
        public List<Guide> guides { get; set; } = new();
        public List<UserPlant> userPlants { get; set; } = new();
        public List<SavedPlant> savedPlants { get; set; } = new();
    }
    public enum WateringFrequency
    {
        Daily = 1,
        TwoTimesAWeek = 2,
        ThreeTimesAweek = 3
    }

    public enum LightRequirement
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}
