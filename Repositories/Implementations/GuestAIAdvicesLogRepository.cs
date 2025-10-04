using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Non_userEntities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class GuestAIAdvicesLogRepository : BaseRepository<GuestAIAdvicesLog>, IGuestAIAdvicesLogRepository
    {
        public GuestAIAdvicesLogRepository(GreenLensDbContext context) : base(context) { }

        public async Task<(IEnumerable<GuestAIAdvicesLog> messages, int totalCount)> GetConversationPagedAsync(
            string guestToken, int? userPlantId, int page, int pageSize)
        {
            var query = _context.Set<GuestAIAdvicesLog>().AsQueryable()
                .Where(l => l.GuestToken == guestToken && !l.isDelete);

            if (userPlantId.HasValue)
                query = query.Where(l => l.UserPlantId == userPlantId);

            var totalCount = await query.CountAsync();
            var messages = await query
                .OrderBy(l => l.createdAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (messages, totalCount);
        }
    }
}
