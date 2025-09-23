namespace ProjectGreenLens.Models.DTOs.ArModel
{
    public class ArModelResponseDto
    {
        public int Id { get; set; }
        public string ModelUrl { get; set; } = null!;
        public string? FileFormat { get; set; }
    }
}
