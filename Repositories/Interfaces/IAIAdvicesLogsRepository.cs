using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IAIAdvicesLogsRepository : IBaseRepository<AIAdvicesLogs>
    {
        Task<List<AIAdvicesLogs>> GetConversation(int userId, int? userPlantId = null, int? plantId = null);
        Task<(List<AIAdvicesLogs> messages, int totalCount)> GetConversationPaged(int userId, int? userPlantId = null, int? plantId = null, int page = 1, int pageSize = 30);
        Task<List<AIAdvicesLogs>> GetLastMessagesByUserAsync(int userId);
    }
}
