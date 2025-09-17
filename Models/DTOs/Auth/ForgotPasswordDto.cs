using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; } = null!;
    }
}
