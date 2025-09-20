namespace ProjectGreenLens.Models.DTOs.PlantPhoto
{
    public class PhotoResponseDto
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public string? Caption { get; set; }
    }
}
