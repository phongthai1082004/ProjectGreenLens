namespace ProjectGreenLens.Models.DTOs.AIAdvice
{
    public class LastMessageDto
    {
        public int? id { get; set; }            // nullable để nhóm null logs
        public int? userPlantId { get; set; }   // nullable
        public string? plantName { get; set; }
        public string? content { get; set; }
        public DateTime? createdAt { get; set; } // null cho log không có cây
    }
}
