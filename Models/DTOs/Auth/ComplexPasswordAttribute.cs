using ProjectGreenLens.Exceptions;
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
                return new ValidationResult(ValidationMessages.RequiredPassword);
            }

            string password = value.ToString()!;
            if (password.Length < 8)
            {
                return new ValidationResult(ValidationMessages.PasswordMinLength);
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return new ValidationResult(ValidationMessages.PasswordRequireLower);
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult(ValidationMessages.PasswordRequireUpper);
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult(ValidationMessages.PasswordRequireDigit);
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]"))
            {
                return new ValidationResult(ValidationMessages.PasswordRequireSpecial);
            }

            return ValidationResult.Success;
        }
    }
}
