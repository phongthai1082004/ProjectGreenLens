using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Reset token is required")]
        public string token { get; set; } = null!;

        [Required(ErrorMessage = "New password is required")]
        [ComplexPassword]
        public string newPassword { get; set; } = null!;
    }
}
