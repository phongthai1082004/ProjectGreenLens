using ProjectGreenLens.Models.DTOs.Guide;
using ProjectGreenLens.Models.DTOs.PlantPhoto;
using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Models.DTOs.Plant
{
    public class PlantResponseDto
    {
        public int Id { get; set; }
        public Guid UniqueGuid { get; set; }
        public string ScientificName { get; set; } = null!;
        public string? CommonName { get; set; }
        public string? Description { get; set; }
        public string? CareInstructions { get; set; }
        public int? PlantCategoryId { get; set; }
        public string? PlantCategoryName { get; set; }
        public bool IsIndoor { get; set; }
        public int WateringFrequency { get; set; }
        public LightRequirement LightRequirement { get; set; }
        public string? SoilType { get; set; }
        public decimal? AveragePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public List<PhotoResponseDto> Photos { get; set; } = new();
        public List<GuideResponseDto> Guides { get; set; } = new();
    }
}
