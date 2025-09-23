using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.AIAdvice
{
    public class AIAdviceUpdateDto
    {
        [Required]
        public int id { get; set; }

        [Required(ErrorMessage = GeminiErrorMessages.CONTENT_REQUIRED)]
        [MaxLength(10000, ErrorMessage = GeminiErrorMessages.CONTENT_TOO_LONG)]
        public string content { get; set; } = null!;

        public int? userPlantId { get; set; }

        [Required]
        [MaxLength(20)]
        public string role { get; set; } = null!;
    }
}