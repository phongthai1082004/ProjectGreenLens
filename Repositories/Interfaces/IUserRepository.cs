using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<UserToken?> GetTokenByValueAsync(string token);
    }
}
