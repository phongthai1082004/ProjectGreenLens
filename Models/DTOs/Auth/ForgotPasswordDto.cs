using ProjectGreenLens.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = ValidationMessages.RequiredPassword)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmail)]
        public string email { get; set; } = null!;
    }
}
