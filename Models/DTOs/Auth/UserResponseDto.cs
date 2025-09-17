namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserResponseDto
    {
        public int id { get; set; }
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public int roleId { get; set; }
    }
}
