using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class AIAdvicesLogsRepository : BaseRepository<AIAdvicesLogs>, IAIAdvicesLogsRepository
    {
        public AIAdvicesLogsRepository(GreenLensDbContext context) : base(context) { }

        public async Task<List<AIAdvicesLogs>> GetConversation(int userId, int? userPlantId = null, int? plantId = null)
        {
            var query = _context.AIAdvicesLogs.AsQueryable()
                .Where(x => x.userId == userId && !x.isDelete);

            if (userPlantId.HasValue)
                query = query.Where(x => x.userPlantId == userPlantId.Value);
            else if (plantId.HasValue)
                query = query.Where(x => x.userPlantId == null && x.plantId == plantId.Value);
            else
                query = query.Where(x => x.userPlantId == null && x.plantId == null);

            return await query.OrderBy(x => x.createdAt).ToListAsync();
        }

        public async Task<(List<AIAdvicesLogs> messages, int totalCount)> GetConversationPaged(int userId, int? userPlantId = null, int? plantId = null, int page = 1, int pageSize = 30)
        {
            var query = _context.AIAdvicesLogs.AsQueryable()
                .Where(x => x.userId == userId && !x.isDelete);

            if (userPlantId.HasValue)
                query = query.Where(x => x.userPlantId == userPlantId.Value);
            else if (plantId.HasValue)
                query = query.Where(x => x.userPlantId == null && x.plantId == plantId.Value);
            else
                query = query.Where(x => x.userPlantId == null && x.plantId == null);

            var totalCount = await query.CountAsync();
            var messages = await query
                .OrderBy(x => x.createdAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (messages, totalCount);
        }

        public async Task<List<AIAdvicesLogs>> GetLastMessagesByUserAsync(int userId)
        {
            var logs = await _context.AIAdvicesLogs
                .Include(x => x.userPlant)
                    .ThenInclude(up => up.plant)
                .Include(x => x.plant)
                .Where(x => x.userId == userId && !x.isDelete)
                .ToListAsync();

            var logsWithUserPlant = logs
                .Where(x => x.userPlantId != null)
                .GroupBy(x => x.userPlantId)
                .Select(g => g.OrderByDescending(x => x.createdAt).First())
                .ToList();

            var logsWithStorePlant = logs
                .Where(x => x.userPlantId == null && x.plantId != null)
                .GroupBy(x => x.plantId)
                .Select(g => g.OrderByDescending(x => x.createdAt).First())
                .ToList();

            var logsFree = logs
                .Where(x => x.userPlantId == null && x.plantId == null)
                .OrderByDescending(x => x.createdAt)
                .Take(1)
                .ToList();

            var lastLogs = logsWithUserPlant
                .Concat(logsWithStorePlant)
                .Concat(logsFree)
                .ToList();

            foreach (var log in lastLogs)
            {
                if (!string.IsNullOrEmpty(log.content) && log.content.Length > 100)
                    log.content = log.content.Substring(0, 100);
            }

            return lastLogs;
        }
    }
}
