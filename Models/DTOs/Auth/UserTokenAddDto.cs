using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserTokenAddDto
    {
        public string token { get; set; } = null!;
        public DateTime expiresAt { get; set; }
        public bool isRevoked { get; set; }
        public UserToken.TokenType type { get; set; }
        public int userId { get; set; }
    }
}
