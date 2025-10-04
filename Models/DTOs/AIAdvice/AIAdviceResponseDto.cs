namespace ProjectGreenLens.Models.DTOs.AIAdvice
{
    public class AIAdviceResponseDto
    {
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public bool isDelete { get; set; }
        public int userId { get; set; }
        public int? userPlantId { get; set; }
        public int? plantId { get; set; }
        public string role { get; set; } = null!;
        public string content { get; set; } = null!;
    }
}