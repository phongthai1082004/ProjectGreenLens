using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Non_userEntities.ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class GuestQuotaRepository : BaseRepository<GuestQuota>, IGuestQuotaRepository
    {
        public GuestQuotaRepository(GreenLensDbContext context) : base(context) { }
        public async Task<GuestQuota?> GetByGuestTokenAsync(string guestToken)
        {
            return await _context.Set<GuestQuota>().FirstOrDefaultAsync(q => q.GuestToken == guestToken);
        }
    }
}
