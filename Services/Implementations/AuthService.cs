using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.DTOs;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private GreenLensDbContext _context;
        private readonly IUserRepository _iUserRepository;

        public AuthService(GreenLensDbContext context, IUserRepository iUserRepository)
        {
            _context = context;
            _iUserRepository = iUserRepository;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            // Find username
            var user = await _context.users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.username == username);

            if (user == null)
                return null;

            // Matching password
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.passwordHash);
            return isPasswordValid ? user : null;
        }

        public async Task<User> CreateUserAsync(RegisterUserDto dto)
        {
            // Find if user is existed
            //if (await _context.users.AnyAsync(u => u.username == dto.Username))
            //{

            //}

            throw new NotImplementedException();
            // Create

            // Save
        }

        public Task<User?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
