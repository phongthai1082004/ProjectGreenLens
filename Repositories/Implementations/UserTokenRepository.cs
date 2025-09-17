using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class UserTokenRepository : BaseRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(GreenLensDbContext context) : base(context)
        {
        }

        public async Task<UserToken?> GetActiveRefreshTokenAsync(string refreshToken, int userId)
        {
            return await _context.UserTokens
                .FirstOrDefaultAsync(t =>
                    t.token == refreshToken &&
                    t.userId == userId &&
                    t.type == UserToken.TokenType.RefreshToken &&
                    !t.isRevoked &&
                    !t.isDelete);
        }

        public async Task<IEnumerable<UserToken>> GetByUserIdAsync(int userId)
        {
            return await _context.UserTokens
                .Where(t => t.userId == userId && !t.isDelete)
                .ToListAsync();
        }

        public async Task RevokeAllTokensAsync(int userId)
        {
            var tokens = await _context.UserTokens
                .Where(t => t.userId == userId && !t.isRevoked && !t.isDelete)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.isRevoked = true;
                token.updatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

    }
}
