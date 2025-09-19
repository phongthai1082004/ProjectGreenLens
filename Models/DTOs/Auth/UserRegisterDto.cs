using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = ValidationMessages.RequiredUsername)]
        [MaxLength(50, ErrorMessage = ValidationMessages.UsernameMaxLength)]
        public string username { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.RequiredEmail)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        [MaxLength(100, ErrorMessage = ValidationMessages.EmailMaxLength)]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.RequiredPassword)]
        [ComplexPassword]
        public string password { get; set; } = null!;

        public int roleId { get; set; }
    }

}
