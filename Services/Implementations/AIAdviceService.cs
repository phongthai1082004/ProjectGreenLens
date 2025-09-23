using AutoMapper;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Models.DTOs.AIAdvice;
using ProjectGreenLens.Models.DTOs.ArModel;
using ProjectGreenLens.Models.DTOs.Conversation;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Services.Implementations
{
    public class AIAdviceService : BaseService<AIAdvicesLogs, AIAdviceResponseDto, AIAdviceAddDto, AIAdviceUpdateDto>, IAIAdviceService
    {
        private readonly IAIAdvicesLogsRepository _aiAdvicesLogsRepository;
        private readonly IBaseRepository<UserPlant> _userPlantRepository;
        private readonly IGeminiService _googleAIService;
        private readonly IMapper _mapper;

        public AIAdviceService(
            IAIAdvicesLogsRepository aiAdvicesLogsRepository,
            IBaseRepository<UserPlant> userPlantRepository,
            IGeminiService googleAIService,
            IMapper mapper) : base(aiAdvicesLogsRepository, mapper)
        {
            _aiAdvicesLogsRepository = aiAdvicesLogsRepository;
            _userPlantRepository = userPlantRepository;
            _googleAIService = googleAIService;
            _mapper = mapper;
        }

        public async Task<AIAdviceResponseDto> GetAdviceAsync(AIAdviceRequestDto request, int userId)
        {
            // Lưu câu hỏi của user
            var userQuestionDto = new AIAdviceAddDto
            {
                userId = userId,
                userPlantId = request.userPlantId,
                role = "user",
                content = request.content
            };

            await CreateAsync(userQuestionDto);

            // Lấy thông tin cây nếu có
            string plantInfo = "Không có thông tin cây cụ thể";
            if (request.userPlantId.HasValue)
            {
                var userPlant = await _userPlantRepository.GetByIdAsync(request.userPlantId.Value);
                if (userPlant == null)
                    throw new KeyNotFoundException(GeminiErrorMessages.USER_PLANT_NOT_FOUND);

                if (userPlant.userId != userId)
                    throw new UnauthorizedAccessException(GeminiErrorMessages.UNAUTHORIZED);

                // Tạo thông tin cây từ UserPlant entity
                plantInfo = $"Cây ID: {userPlant.id}, " +
                           $"Biệt danh: {userPlant.nickname ?? "Không có"}, " +
                           $"Tình trạng sức khỏe: {userPlant.healthStatus}, " +
                           $"Vị trí hiện tại: {userPlant.currentLocation ?? "Không rõ"}, " +
                           $"Ngày có cây: {userPlant.acquiredDate:dd/MM/yyyy}, " +
                           $"Ghi chú: {userPlant.notes ?? "Không có ghi chú"}";
            }

            // Gọi Google AI để lấy lời khuyên
            var aiResponse = await _googleAIService.GetPlantAdviceAsync(request.content, plantInfo);

            // Lưu câu trả lời của AI
            var assistantResponseDto = new AIAdviceAddDto
            {
                userId = userId,
                userPlantId = request.userPlantId,
                role = "assistant",
                content = aiResponse
            };

            var savedResponse = await CreateAsync(assistantResponseDto);
            return savedResponse;
        }

        public async Task<IEnumerable<AIAdviceResponseDto>> GetUserConversationAsync(int userId, int? userPlantId = null)
        {
            var conversations = await _aiAdvicesLogsRepository.GetConversation(userId, userPlantId);
            return _mapper.Map<IEnumerable<AIAdviceResponseDto>>(conversations);
        }

        public async Task<ConversationResponseDto> GetUserConversationPagedAsync(int userId, ConversationRequestDto request)
        {
            var (messages, totalCount) = await _aiAdvicesLogsRepository.GetConversationPaged(
                userId,
                request.userPlantId,
                request.page,
                request.pageSize);

            var totalPages = (int)Math.Ceiling((double)totalCount / request.pageSize);
            var hasMore = request.page < totalPages;

            var messageDtos = _mapper.Map<IEnumerable<AIAdviceResponseDto>>(messages);

            return new ConversationResponseDto
            {
                messages = messageDtos,
                hasMore = hasMore,
                currentPage = request.page,
                totalCount = totalCount,
                totalPages = totalPages
            };
        }
    }
}


