using ProjectGreenLens.Models.Non_userEntities.ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IGuestQuotaRepository : IBaseRepository<GuestQuota>
    {
        Task<GuestQuota?> GetByGuestTokenAsync(string guestToken);
    }
}
