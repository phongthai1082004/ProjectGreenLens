using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class AIAdvicesLogsRepository : BaseRepository<AIAdvicesLogs>, IAIAdvicesLogsRepository
    {
        public AIAdvicesLogsRepository(GreenLensDbContext context) : base(context)
        {
        }

        public async Task<List<AIAdvicesLogs>> GetConversation(int userId, int? plantId = null)
        {
            var query = _context.AIAdvicesLogs.AsQueryable();

            query = query.Where(x => x.userId == userId && !x.isDelete);

            if (plantId.HasValue)
            {
                query = query.Where(x => x.userPlantId == plantId.Value);
            }

            query = query.OrderBy(x => x.createdAt);
            return await query.ToListAsync();
        }

        public async Task<(List<AIAdvicesLogs> messages, int totalCount)> GetConversationPaged(int userId, int? plantId = null, int page = 1, int pageSize = 30)
        {
            var query = _context.AIAdvicesLogs.AsQueryable();

            query = query.Where(x => x.userId == userId && !x.isDelete);

            if (plantId.HasValue)
            {
                query = query.Where(x => x.userPlantId == plantId.Value);
            }

            var totalCount = await query.CountAsync();

            var totalItemsToTake = page * pageSize;

            var messages = await query
                .OrderBy(x => x.createdAt)
                .Take(totalItemsToTake)
                .ToListAsync();

            return (messages, totalCount);
        }


        public async Task<List<AIAdvicesLogs>> GetLastMessagesByUserAsync(int userId)
        {
            var logs = await _context.AIAdvicesLogs
                .Include(x => x.userPlant)
                    .ThenInclude(up => up.plant)
                .Where(x => x.userId == userId && !x.isDelete)
                .ToListAsync();

            var lastLogsPerPlant = logs
                .GroupBy(x => x.userPlantId)
                .Select(g => g.OrderByDescending(x => x.createdAt).First())
                .ToList();

            foreach (var log in lastLogsPerPlant)
            {
                if (!string.IsNullOrEmpty(log.content) && log.content.Length > 100)
                    log.content = log.content.Substring(0, 100);
            }

            return lastLogsPerPlant;
        }
    }
}
