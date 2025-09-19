using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = ValidationMessages.RequiredEmail)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        public string email { get; set; } = null!;

        [Required(ErrorMessage = ValidationMessages.RequiredPassword)]
        public string password { get; set; } = null!;
    }
}
