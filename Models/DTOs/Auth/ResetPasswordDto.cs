using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = ValidationMessages.RequiredRefreshToken)]
        public string token { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.RequiredPassword)]
        [ComplexPassword]
        public string newPassword { get; set; } = null!;
    }
}
