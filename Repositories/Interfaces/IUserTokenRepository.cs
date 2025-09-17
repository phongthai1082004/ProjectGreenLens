using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IUserTokenRepository : IBaseRepository<UserToken>
    {
        Task<UserToken?> GetActiveRefreshTokenAsync(string refreshToken, int userId);
        Task<IEnumerable<UserToken>> GetByUserIdAsync(int userId);
        Task RevokeAllTokensAsync(int userId);
    }
}
