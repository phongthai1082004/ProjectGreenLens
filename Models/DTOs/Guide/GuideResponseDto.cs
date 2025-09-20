namespace ProjectGreenLens.Models.DTOs.Guide
{
    public class GuideResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Content { get; set; }
    }
}
