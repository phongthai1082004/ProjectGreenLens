using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.GuestAIAdvice
{
    public class GuestAIAdviceRequestDto
    {
        [Required]
        public string GuestToken { get; set; } = null!;
        public int? UserPlantId { get; set; }
        [Required]
        public string Content { get; set; } = null!;
    }
}
