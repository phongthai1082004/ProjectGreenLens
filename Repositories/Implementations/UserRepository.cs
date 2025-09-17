using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GreenLensDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>()
                .Include(u => u.role)
                .FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task<UserToken?> GetTokenByValueAsync(string token)
        {
            return await _context.Set<UserToken>()
                .FirstOrDefaultAsync(t => t.token == token);
        }
    }
}
