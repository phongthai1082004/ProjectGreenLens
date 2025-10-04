using ProjectGreenLens.Models.DTOs.GuestAIAdvice;
using ProjectGreenLens.Models.Non_userEntities;

namespace ProjectGreenLens.Services.Interfaces
{
    public interface IGuestAIAdviceService : IBaseService<GuestAIAdvicesLog, GuestAIAdviceResponseDto, GuestAIAdviceRequestDto, GuestAIAdviceRequestDto>
    {
        Task<GuestAIAdviceResponseDto> GetAdviceAsync(GuestAIAdviceRequestDto request);
        Task<GuestConversationPagedResponseDto> GetConversationPagedAsync(string guestToken, GuestAIAdvicePagedRequestDto request);
    }
}
