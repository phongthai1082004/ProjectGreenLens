using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly GreenLensDbContext _context;

        public UserRepository(GreenLensDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.users.Include(u => u.role).FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.users.Include(u => u.role).FirstOrDefaultAsync(u => u.username == username);
        }
    }
}
