namespace ProjectGreenLens.Models.DTOs.AIAdvice
{
    public class LastMessageDto
    {
        public int? id { get; set; }
        public int? userPlantId { get; set; }
        public int? plantId { get; set; }
        public string? plantName { get; set; }
        public string? content { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
