using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Entities
{
    public class PlantCategory : BaseEntity
    {
        [Required, MaxLength(100)]
        public string name { get; set; } = null!;

        [MaxLength(500)]
        public string? description { get; set; }

        public List<Plant> plants { get; set; } = new();
    }
}
