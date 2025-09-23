using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Models.DTOs.Auth;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProjectGreenLens.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _tokenRepository;
        private readonly IBaseRepository<Role> _roleRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<AuthService> _logger;
        private readonly string _frontendUrl;

        public AuthService(
            IUserRepository userRepository,
            IUserTokenRepository tokenRepository,
            IBaseRepository<Role> roleRepository,
            IEmailService emailService,
            IMapper mapper,
            IOptions<JwtSettings> jwtOptions,
            ILogger<AuthService> logger,
            IConfiguration config)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _roleRepository = roleRepository;
            _emailService = emailService;
            _mapper = mapper;
            _jwtSettings = jwtOptions.Value;
            _logger = logger;
            _frontendUrl = config["AppSettings:FrontendUrl"];
        }

        // Private

        // Private methods
        private string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Name, user.username),
                 new Claim(ClaimTypes.Role, user.roleId.ToString()),
                new Claim("guid", user.uniqueGuid.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateSecureToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var tokenBytes = new byte[32];
            rng.GetBytes(tokenBytes);
            return Convert.ToBase64String(tokenBytes);
        }

        private async Task SendVerificationEmailAsync(string email, string token)
        {
            var subject = "Email Verification - ProjectGreenLens";
            var body = $@"
            <h2>Email Verification</h2>
            <p>Please click the link below to verify your email address:</p>
            <a href='{_frontendUrl}/verify-email?token={token}'>Verify Email</a>
            <p>This link will expire in 24 hours.</p>
        ";

            await _emailService.SendEmailAsync(email, subject, body);
        }

        private async Task SendPasswordResetEmailAsync(string email, string token)
        {
            var subject = "Password Reset - ProjectGreenLens";
            var body = $@"
            <h2>Password Reset</h2>
            <p>Please click the link below to reset your password:</p>
            <a href='{_frontendUrl}/reset-password?token={token}'>Reset Password</a>
            <p>This link will expire in 1 hour.</p>
        ";

            await _emailService.SendEmailAsync(email, subject, body);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<UserResponseDto> RegisterAsync(UserRegisterDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to register user with email: {Email}", dto.email);

                // Check if user already exist
                var existingUser = await _userRepository.GetByEmailAsync(dto.email);
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Tài Khoản đã tổn tại!");
                }

                // Check if role exists
                var role = await _roleRepository.GetByIdAsync(dto.roleId);
                if (role == null)
                {
                    throw new NotFoundException("Role not found");
                }
                var hashedPassword = HashPassword(dto.password);

                // Add User
                var userToAdd = new UserAddDto
                {
                    username = dto.username,
                    email = dto.email,
                    passwordHash = hashedPassword,
                    roleId = dto.roleId
                };

                var user = _mapper.Map<User>(userToAdd);
                user.isEmailVerified = false;
                user.createdAt = DateTime.UtcNow;
                user.updatedAt = DateTime.UtcNow;

                // Returned Json
                var createdUser = await _userRepository.CreateAsync(user);

                // Generate Token

                var verificationToken = GenerateSecureToken();
                var tokenEntity = new UserToken
                {
                    token = verificationToken,
                    expiresAt = DateTime.UtcNow.AddHours(24),
                    isRevoked = false,
                    type = UserToken.TokenType.VerifyEmail,
                    userId = createdUser.id,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                };

                await _tokenRepository.CreateAsync(tokenEntity);

                // Send verification email
                await SendVerificationEmailAsync(createdUser.email, verificationToken);

                var response = _mapper.Map<UserResponseDto>(createdUser);

                _logger.LogInformation("User registered successfully with ID: {UserId}", createdUser.id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while registering user with email: {Email}", dto.email);
                throw;
            }
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to login user with email: {Email}", dto.email);
                // Get user by email
                var user = await _userRepository.GetByEmailAsync(dto.email);
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Mật khẩu không đúng!");
                }
                // Verify password
                if (!VerifyPassword(dto.password, user.passwordHash))
                {
                    throw new UnauthorizedAccessException("Email hoặc mật khẩu không hợp lệ!");
                }
                if (!user.isEmailVerified)
                {
                    throw new UnauthorizedAccessException("Hãy xác thực tải khoản trước khi sử dụng!");
                }
                // Generate tokens
                var accessToken = GenerateAccessToken(user);
                var refreshToken = GenerateSecureToken();

                var tokenEntity = new UserToken
                {
                    token = refreshToken,
                    expiresAt = DateTime.UtcNow.AddDays(7),
                    isRevoked = false,
                    type = UserToken.TokenType.RefreshToken,
                    userId = user.id,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                };

                await _tokenRepository.CreateAsync(tokenEntity);
                var response = new AuthResponseDto
                {
                    accessToken = accessToken,
                    refreshToken = refreshToken,
                    expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    user = _mapper.Map<UserResponseDto>(user)
                };
                _logger.LogInformation("User logged in successfully with ID: {UserId}", user.id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging in user with email: {Email}", dto.email);
                throw;
            }
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to refresh token");

                var tokenEntities = await _tokenRepository.GetAllAsync();
                var tokenEntity = tokenEntities.FirstOrDefault(t =>
                   t.token == dto.refreshToken &&
                   t.type == UserToken.TokenType.RefreshToken &&
                   !t.isRevoked &&
                   t.expiresAt > DateTime.UtcNow);

                if (tokenEntity == null)
                {
                    throw new UnauthorizedAccessException("Invalid or expired refresh token");
                }
                var user = await _userRepository.GetByIdAsync(tokenEntity.userId);
                if (user == null)
                {
                    throw new NotFoundException("User not found");
                }

                // Revoke old refresh token
                tokenEntity.isRevoked = true;
                tokenEntity.updatedAt = DateTime.UtcNow;
                await _tokenRepository.UpdateAsync(tokenEntity);

                // Generate new tokens
                var newAccessToken = GenerateAccessToken(user);
                var newRefreshToken = GenerateSecureToken();

                // Save new refresh token
                var newTokenEntity = new UserToken
                {
                    token = newRefreshToken,
                    expiresAt = DateTime.UtcNow.AddDays(7),
                    isRevoked = false,
                    type = UserToken.TokenType.RefreshToken,
                    userId = user.id,
                    createdAt = DateTime.UtcNow,
                    updatedAt = DateTime.UtcNow
                };

                await _tokenRepository.CreateAsync(newTokenEntity);

                var response = new AuthResponseDto
                {
                    accessToken = newAccessToken,
                    refreshToken = newRefreshToken,
                    expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                    user = _mapper.Map<UserResponseDto>(user)
                };

                _logger.LogInformation("Token refreshed successfully for user ID: {UserId}", user.id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while refreshing token");
                throw;
            }
        }

        public async Task LogoutAsync(int userId, RefreshTokenDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to logout user with ID: {UserId}", userId);

                // Validation
                if (string.IsNullOrWhiteSpace(dto?.refreshToken))
                {
                    _logger.LogWarning("Empty refresh token for user ID: {UserId}", userId);
                    throw new ArgumentException("Refresh token is required");
                }

                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid user ID: {UserId}", userId);
                    throw new ArgumentException("Invalid user ID");
                }

                // Sử dụng method đã có sẵn trong UserTokenRepository
                var tokenEntity = await _tokenRepository.GetActiveRefreshTokenAsync(dto.refreshToken, userId);

                if (tokenEntity == null)
                {
                    _logger.LogWarning("Active refresh token not found for user ID: {UserId}", userId);
                    throw new UnauthorizedAccessException("Invalid or expired refresh token");
                }

                // Revoke token
                tokenEntity.isRevoked = true;
                tokenEntity.updatedAt = DateTime.UtcNow; // BaseEntity có updatedAt

                await _tokenRepository.UpdateAsync(tokenEntity);

                _logger.LogInformation("User logged out successfully with ID: {UserId}", userId);
            }
            catch (ArgumentException)
            {
                throw; // Re-throw validation errors
            }
            catch (UnauthorizedAccessException)
            {
                throw; // Re-throw auth errors
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging out user with ID: {UserId}", userId);
                throw new InvalidOperationException("Logout failed due to system error", ex);
            }
        }

        public async Task RequestPasswordResetAsync(ForgotPasswordDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to request password reset for email: {Email}", dto.email);

                var user = await _userRepository.GetByEmailAsync(dto.email);
                if (user == null)
                {
                    // Don't reveal if email exists or not for security reasons
                    _logger.LogWarning("Password reset requested for non-existing email: {Email}", dto.email);
                    return;
                }

                // Revoke any existing password reset tokens for this user
                var existingTokens = await _tokenRepository.GetAllAsync();
                var userResetTokens = existingTokens.Where(t =>
                    t.userId == user.id &&
                    t.type == UserToken.TokenType.ResetPassword &&
                    !t.isRevoked).ToList();

                foreach (var existingToken in userResetTokens)
                {
                    existingToken.isRevoked = true;
                    await _tokenRepository.UpdateAsync(existingToken);
                }

                // Generate password reset token
                var resetToken = GenerateSecureToken();
                var tokenEntity = new UserToken
                {
                    token = resetToken,
                    expiresAt = DateTime.UtcNow.AddHours(1), // 1 hour expiry
                    isRevoked = false,
                    type = UserToken.TokenType.ResetPassword,
                    userId = user.id,
                    createdAt = DateTime.UtcNow
                };

                await _tokenRepository.CreateAsync(tokenEntity);

                // Send password reset email
                await SendPasswordResetEmailAsync(user.email, resetToken);

                _logger.LogInformation("Password reset token sent for user ID: {UserId}", user.id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while requesting password reset for email: {Email}", dto.email);
                throw;
            }
        }

        public async Task ResetPasswordAsync(ResetPasswordDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to reset password with token");

                // Find the password reset token
                var tokenEntities = await _tokenRepository.GetAllAsync();
                var tokenEntity = tokenEntities.FirstOrDefault(t =>
                    t.token == dto.token &&
                    t.type == UserToken.TokenType.ResetPassword &&
                    !t.isRevoked &&
                    t.expiresAt > DateTime.UtcNow);

                if (tokenEntity == null)
                {
                    throw new UnauthorizedAccessException("Invalid or expired reset token");
                }

                // Get user
                var user = await _userRepository.GetByIdAsync(tokenEntity.userId);
                if (user == null)
                {
                    throw new NotFoundException("Người dùng không tồn tại!");
                }

                // Hash new password and update trực tiếp
                var hashedPassword = HashPassword(dto.newPassword);
                user.passwordHash = hashedPassword;
                user.updatedAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                // Revoke the reset token
                tokenEntity.isRevoked = true;
                await _tokenRepository.UpdateAsync(tokenEntity);

                // Revoke all refresh tokens for this user (force re-login)
                var allTokens = await _tokenRepository.GetAllAsync();
                var userRefreshTokens = allTokens.Where(t =>
                    t.userId == user.id &&
                    t.type == UserToken.TokenType.RefreshToken &&
                    !t.isRevoked).ToList();

                foreach (var refreshToken in userRefreshTokens)
                {
                    refreshToken.isRevoked = true;
                    await _tokenRepository.UpdateAsync(refreshToken);
                }

                _logger.LogInformation("Password reset successfully for user ID: {UserId}", user.id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while resetting password");
                throw;
            }
        }

        public async Task VerifyEmailAsync(string token)
        {
            try
            {
                _logger.LogInformation("Attempting to verify email with token");

                var tokenEntities = await _tokenRepository.GetAllAsync();
                var tokenEntity = tokenEntities.FirstOrDefault(t =>
                    t.token == token &&
                    t.type == UserToken.TokenType.VerifyEmail &&
                    !t.isRevoked &&
                    t.expiresAt > DateTime.UtcNow);

                if (tokenEntity == null)
                {
                    throw new UnauthorizedAccessException("Invalid or expired verification token");
                }

                var user = await _userRepository.GetByIdAsync(tokenEntity.userId);
                if (user == null)
                {
                    throw new NotFoundException("User not found");
                }

                user.isEmailVerified = true;
                user.updatedAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                tokenEntity.isRevoked = true;
                await _tokenRepository.UpdateAsync(tokenEntity);

                _logger.LogInformation("Email verified successfully for user ID: {UserId}", user.id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while verifying email");
                throw;
            }
        }
    }
}
