﻿using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }

}
