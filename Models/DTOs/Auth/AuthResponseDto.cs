namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string accessToken { get; set; } = null!;
        public string refreshToken { get; set; } = null!;
        public DateTime expiresAt { get; set; }
        public UserResponseDto user { get; set; } = null!;
    }
}
