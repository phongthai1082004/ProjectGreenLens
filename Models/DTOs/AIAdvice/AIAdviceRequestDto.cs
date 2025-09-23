using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.ArModel
{
    public class AIAdviceRequestDto
    {
        [Required(ErrorMessage = GeminiErrorMessages.CONTENT_REQUIRED)]
        [MaxLength(10000, ErrorMessage = GeminiErrorMessages.CONTENT_TOO_LONG)]
        public string content { get; set; } = null!;

        public int? userPlantId { get; set; }
    }
}
