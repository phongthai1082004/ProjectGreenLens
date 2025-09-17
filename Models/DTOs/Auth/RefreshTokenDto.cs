using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{

    public class RefreshTokenDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string refreshToken { get; set; } = null!;
    }
}
