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
            var apiResponse = await _googleAIService.GetPlantAdviceAsync(request.content, plantInfo);

            // Lưu câu trả lời của AI
            var assistantResponseDto = new AIAdviceAddDto
            {
                userId = userId,
                userPlantId = request.userPlantId,
                role = "assistant",
                content = apiResponse
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

        public async Task<List<LastMessageDto>> GetLastMessagesByUserAsync(int userId)
        {
            var logs = await _aiAdvicesLogsRepository.GetLastMessagesByUserAsync(userId);

            // 1. Nhóm theo cây (có userPlant)
            var logsWithPlant = logs
                .Where(l => l.userPlant != null)
                .GroupBy(l => l.userPlantId)
                .Select(g => g.OrderByDescending(l => l.createdAt).First())
                .Select(log => new LastMessageDto
                {
                    id = log.id,
                    userPlantId = log.userPlantId,
                    plantName = log.userPlant?.plant?.scientificName,
                    content = log.content != null && log.content.Length > 100
                                ? log.content.Substring(0, 100)
                                : log.content,
                    createdAt = log.createdAt
                });

            // 2. Nhóm log không có cây → lấy bản mới nhất
            var nullPlantLog = logs
                .Where(l => l.userPlant == null)
                .OrderByDescending(l => l.createdAt)
                .FirstOrDefault();

            LastMessageDto? nullPlantDto = null;
            if (nullPlantLog != null)
            {
                nullPlantDto = new LastMessageDto
                {
                    id = nullPlantLog.id,
                    userPlantId = null,
                    plantName = null,
                    content = nullPlantLog.content != null && nullPlantLog.content.Length > 100
                                ? nullPlantLog.content.Substring(0, 100)
                                : nullPlantLog.content,
                    createdAt = nullPlantLog.createdAt
                };
            }

            // Kết hợp
            var result = logsWithPlant.ToList();
            if (nullPlantDto != null)
                result.Add(nullPlantDto);

            return result;
        }

    }
}


