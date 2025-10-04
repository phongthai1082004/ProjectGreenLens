namespace ProjectGreenLens.Models.DTOs.GuestAIAdvice
{
    public class GuestAIAdviceResponseDto
    {
        public int Id { get; set; }
        public string GuestToken { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? UserPlantId { get; set; }
    }
}
