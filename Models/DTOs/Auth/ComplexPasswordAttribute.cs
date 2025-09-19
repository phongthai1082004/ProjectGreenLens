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
                return new ValidationResult(ValidationMessages.PasswordTooShort);
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return new ValidationResult(ValidationMessages.PasswordMissingLowercase);
            }

            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return new ValidationResult(ValidationMessages.PasswordMissingUppercase);
            }

            if (!Regex.IsMatch(password, @"\d"))
            {
                return new ValidationResult(ValidationMessages.PasswordMissingDigit);
            }

            if (!Regex.IsMatch(password, @"[!@#$%^&*(),.?""':;{}|<>]"))
            {
                return new ValidationResult(ValidationMessages.PasswordMissingSpecialChar);
            }

            return ValidationResult.Success;
        }
    }
}
