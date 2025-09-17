using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(50, ErrorMessage = "Username cannot exceed 50 characters")]
        public string username { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [ComplexPassword]
        public string password { get; set; } = null!;

        public int roleId { get; set; }
    }

}
