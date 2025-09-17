using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ProjectGreenLens.Models.DTOs.Auth
{
    public class ComplexPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Password is required.");
            }

            string password = value.ToString()!;
            if (password.Length < 8)
            {
                return new ValidationResult("Password must be at least 8 characters long.");
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.");
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult("Password must contain at least one number.");
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]"))
            {
                return new ValidationResult("Password must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}
