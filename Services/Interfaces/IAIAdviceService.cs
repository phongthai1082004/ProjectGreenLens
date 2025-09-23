using ProjectGreenLens.Models.DTOs.AIAdvice;
using ProjectGreenLens.Models.DTOs.ArModel;
using ProjectGreenLens.Models.DTOs.Conversation;
using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Services.Interfaces
{
    public interface IAIAdviceService : IBaseService<AIAdvicesLogs, AIAdviceResponseDto, AIAdviceAddDto, AIAdviceUpdateDto>
    {
        Task<AIAdviceResponseDto> GetAdviceAsync(AIAdviceRequestDto request, int userId);
        Task<IEnumerable<AIAdviceResponseDto>> GetUserConversationAsync(int userId, int? userPlantId = null);
        Task<ConversationResponseDto> GetUserConversationPagedAsync(int userId, ConversationRequestDto request);
    }
}
