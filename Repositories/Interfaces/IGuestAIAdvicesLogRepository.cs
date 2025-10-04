using ProjectGreenLens.Models.Non_userEntities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IGuestAIAdvicesLogRepository : IBaseRepository<GuestAIAdvicesLog>
    {
        Task<(IEnumerable<GuestAIAdvicesLog> messages, int totalCount)> GetConversationPagedAsync(
            string guestToken, int? userPlantId, int page, int pageSize);
    }
}
