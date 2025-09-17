using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Models.DTOs.Auth;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;
using System.Security.Claims;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<UserResponseDto>>> RegisterAsync([FromBody] UserRegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(ApiResponse<UserResponseDto>.Ok(result,
                "User registered successfully. Please check your email to verify your account."));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> LoginAsync([FromBody] UserLoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Login successful"));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> RefreshTokenAsync([FromBody] RefreshTokenDto dto)
        {
            var result = await _authService.RefreshTokenAsync(dto);
            return Ok(ApiResponse<AuthResponseDto>.Ok(result, "Token refreshed successfully"));
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<string>>> LogoutAsync([FromBody] RefreshTokenDto dto)
        {
            var userId = GetCurrentUserId();
            await _authService.LogoutAsync(userId, dto);
            return Ok(ApiResponse<string>.Ok("", "Logged out successfully"));
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ApiResponse<string>>> RequestPasswordResetAsync([FromBody] ForgotPasswordDto dto)
        {
            await _authService.RequestPasswordResetAsync(dto);
            return Ok(ApiResponse<string>.Ok("", "If the email exists, a password reset link has been sent"));
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse<string>>> ResetPasswordAsync([FromBody] ResetPasswordDto dto)
        {
            await _authService.ResetPasswordAsync(dto);
            return Ok(ApiResponse<string>.Ok("", "Password reset successfully"));
        }

        [HttpGet("verify-email")]
        public async Task<ActionResult<ApiResponse<string>>> VerifyEmailAsync([FromQuery] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(ApiResponse<string>.Fail("Token is required"));
            }

            await _authService.VerifyEmailAsync(token);
            return Ok(ApiResponse<string>.Ok("", "Email verified successfully"));
        }

        [HttpGet("me")]
        [Authorize]
        public ActionResult<ApiResponse<object>> GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var userInfo = new
            {
                Id = userId,
                Email = userEmail,
                Username = userName,
                Role = userRole
            };

            return Ok(ApiResponse<object>.Ok(userInfo, "User information retrieved successfully"));
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID in token");
            }
            return userId;
        }
    }
}
