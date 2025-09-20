using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Plant
{
    public class PlantAddDto
    {
        [Required(ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.RequiredScientificName)]
        [MaxLength(150, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.ScientificNameMaxLength)]
        public string ScientificName { get; set; } = null!;

        [MaxLength(150, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.CommonNameMaxLength)]
        public string? CommonName { get; set; }

        [MaxLength(2000, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.DescriptionMaxLength)]
        public string? Description { get; set; }

        [MaxLength(2000, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.CareInstructionsMaxLength)]
        public string? CareInstructions { get; set; }

        public int? PlantCategoryId { get; set; }

        public bool IsIndoor { get; set; }

        [Required(ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.RequiredWateringFrequency)]
        [Range(1, int.MaxValue, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.InvalidWateringFrequency)]
        public int WateringFrequency { get; set; }

        [Required(ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.RequiredLightRequirement)]
        [Range(1, int.MaxValue, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.InvalidLightRequirement)]
        public int LightRequirement { get; set; }

        [MaxLength(100, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.SoilTypeMaxLength)]
        public string? SoilType { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = ProjectGreenLens.Exceptions.PlantValidationMessages.InvalidAveragePrice)]
        public decimal? AveragePrice { get; set; }
    }
}
