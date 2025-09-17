using ProjectGreenLens.Models.DTOs.Auth;

namespace ProjectGreenLens.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserResponseDto> RegisterAsync(UserRegisterDto dto);
        Task<AuthResponseDto> LoginAsync(UserLoginDto dto);
        Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto);
        Task LogoutAsync(int userId, RefreshTokenDto dto);
        Task RequestPasswordResetAsync(ForgotPasswordDto dto);
        Task ResetPasswordAsync(ResetPasswordDto dto);
        Task VerifyEmailAsync(string token);
    }
}
