using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{

    public class RefreshTokenDto
    {
        [Required(ErrorMessage = ValidationMessages.RequiredRefreshToken)]
        public string refreshToken { get; set; } = null!;
    }
}
