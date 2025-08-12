using ProjectGreenLens.Models.DTOs;

namespace ProjectGreenLens.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password);
        Task<UserResponseDto> RegisterAsync(RegisterUserDto registerDto);
        Task<bool> LogoutAsync(string userId);
        Task<bool> ForgotPasswordAsync(string email);
    }
}
