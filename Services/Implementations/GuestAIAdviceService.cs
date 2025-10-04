using AutoMapper;
using ProjectGreenLens.Models.DTOs.GuestAIAdvice;
using ProjectGreenLens.Models.Non_userEntities;
using ProjectGreenLens.Models.Non_userEntities.ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Services.Implementations
{
    public class GuestAIAdviceService
         : BaseService<GuestAIAdvicesLog, GuestAIAdviceResponseDto, GuestAIAdviceRequestDto, GuestAIAdviceRequestDto>,
           IGuestAIAdviceService
    {
        private readonly IGuestAIAdvicesLogRepository _logRepository;
        private readonly IGuestQuotaRepository _quotaRepository;
        private readonly IGeminiService _googleAIService;
        private readonly IMapper _mapper;
        private const int MaxQuota = 5;

        public GuestAIAdviceService(
            IGuestAIAdvicesLogRepository logRepository,
            IGuestQuotaRepository quotaRepository,
            IGeminiService googleAIService,
            IMapper mapper) : base(logRepository, mapper)
        {
            _logRepository = logRepository;
            _quotaRepository = quotaRepository;
            _googleAIService = googleAIService;
            _mapper = mapper;
        }

        public async Task<GuestAIAdviceResponseDto> GetAdviceAsync(GuestAIAdviceRequestDto request)
        {
            // Quota xử lý
            var quota = await _quotaRepository.GetByGuestTokenAsync(request.GuestToken);
            if (quota == null)
            {
                quota = new GuestQuota
                {
                    GuestToken = request.GuestToken,
                    UsedCount = 1,
                    LastUsedAt = DateTime.UtcNow
                };
                await _quotaRepository.CreateAsync(quota);
            }
            else
            {
                if (quota.UsedCount >= MaxQuota)
                    throw new InvalidOperationException("Bạn đã hết lượt dùng thử cho guest!");
                quota.UsedCount++;
                quota.LastUsedAt = DateTime.UtcNow;
                await _quotaRepository.UpdateAsync(quota);
            }

            // Save guest question
            var userLog = _mapper.Map<GuestAIAdvicesLog>(request);
            userLog.Role = "user";
            userLog.createdAt = DateTime.UtcNow;
            await _logRepository.CreateAsync(userLog);

            // AI answer
            string plantInfo = "No specific plant info";
            var aiResponse = await _googleAIService.GetPlantAdviceAsync(request.Content, plantInfo);

            var assistantLog = new GuestAIAdvicesLog
            {
                GuestToken = request.GuestToken,
                Role = "assistant",
                Content = aiResponse,
                createdAt = DateTime.UtcNow,
                UserPlantId = request.UserPlantId
            };
            var savedLog = await _logRepository.CreateAsync(assistantLog);

            return _mapper.Map<GuestAIAdviceResponseDto>(savedLog);
        }

        public async Task<GuestConversationPagedResponseDto> GetConversationPagedAsync(
            string guestToken, GuestAIAdvicePagedRequestDto request)
        {
            var (messages, totalCount) = await _logRepository.GetConversationPagedAsync(
                guestToken,
                request.UserPlantId,
                request.Page,
                request.PageSize);

            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);
            var hasMore = request.Page < totalPages;
            var messageDtos = _mapper.Map<IEnumerable<GuestAIAdviceResponseDto>>(messages);

            return new GuestConversationPagedResponseDto
            {
                Messages = messageDtos,
                HasMore = hasMore,
                CurrentPage = request.Page,
                TotalCount = totalCount,
                TotalPages = totalPages
            };
        }
    }
}
