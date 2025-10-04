using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Models.DTOs.AIAdvice;
using ProjectGreenLens.Models.DTOs.Conversation;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Services.Implementations
{
    [Authorize(Roles = "2")]
    public class AIAdviceService : BaseService<AIAdvicesLogs, AIAdviceResponseDto, AIAdviceAddDto, AIAdviceUpdateDto>, IAIAdviceService
    {
        private readonly IAIAdvicesLogsRepository _aiAdvicesLogsRepository;
        private readonly IBaseRepository<UserPlant> _userPlantRepository;
        private readonly IBaseRepository<Plant> _plantRepository;
        private readonly IGeminiService _googleAIService;
        private readonly IMapper _mapper;

        public AIAdviceService(
            IAIAdvicesLogsRepository aiAdvicesLogsRepository,
            IBaseRepository<UserPlant> userPlantRepository,
            IBaseRepository<Plant> plantRepository,
            IGeminiService googleAIService,
            IMapper mapper) : base(aiAdvicesLogsRepository, mapper)
        {
            _aiAdvicesLogsRepository = aiAdvicesLogsRepository;
            _userPlantRepository = userPlantRepository;
            _plantRepository = plantRepository;
            _googleAIService = googleAIService;
            _mapper = mapper;
        }

        public async Task<AIAdviceResponseDto> GetAdviceAsync(AIAdviceRequestDto request, int userId)
        {
            string plantInfo = "Không có thông tin cây cụ thể";
            int? userPlantId = request.userPlantId;
            int? plantId = request.plantId;

            if (userPlantId.HasValue)
            {
                var userPlant = await _userPlantRepository.GetByIdAsync(userPlantId.Value);
                if (userPlant == null)
                    throw new KeyNotFoundException(GeminiErrorMessages.USER_PLANT_NOT_FOUND);
                if (userPlant.userId != userId)
                    throw new UnauthorizedAccessException(GeminiErrorMessages.UNAUTHORIZED);

                plantId = userPlant.plantId;
                plantInfo = $"Cây ID: {userPlant.id}, " +
                            $"Biệt danh: {userPlant.nickname ?? "Không có"}, " +
                            $"Tình trạng sức khỏe: {userPlant.healthStatus}, " +
                            $"Vị trí hiện tại: {userPlant.currentLocation ?? "Không rõ"}, " +
                            $"Ngày có cây: {userPlant.acquiredDate:dd/MM/yyyy}, " +
                            $"Ghi chú: {userPlant.notes ?? "Không có ghi chú"}";
            }
            else if (plantId.HasValue)
            {
                var plant = await _plantRepository.GetByIdAsync(plantId.Value);
                if (plant == null)
                    throw new KeyNotFoundException("Cây bạn hỏi không tồn tại trong cửa hàng.");

                plantInfo = $"Tên khoa học: {plant.scientificName}, " +
                            $"Tên thường gọi: {plant.commonName ?? "Không có"}, " +
                            $"Mô tả: {plant.description ?? "Không có"}, " +
                            $"Hướng dẫn chăm sóc: {plant.careInstructions ?? "Không có"}";
            }

            // Lưu câu hỏi của user
            var userQuestionDto = new AIAdviceAddDto
            {
                userId = userId,
                userPlantId = userPlantId,
                plantId = plantId,
                role = "user",
                content = request.content
            };
            await CreateAsync(userQuestionDto);

            var apiResponse = await _googleAIService.GetPlantAdviceAsync(request.content, plantInfo);

            var assistantResponseDto = new AIAdviceAddDto
            {
                userId = userId,
                userPlantId = userPlantId,
                plantId = plantId,
                role = "assistant",
                content = apiResponse
            };
            var savedResponse = await CreateAsync(assistantResponseDto);
            return savedResponse;
        }

        public async Task<IEnumerable<AIAdviceResponseDto>> GetUserConversationAsync(int userId, int? userPlantId = null, int? plantId = null)
        {
            var conversations = await _aiAdvicesLogsRepository.GetConversation(userId, userPlantId, plantId);
            return _mapper.Map<IEnumerable<AIAdviceResponseDto>>(conversations);
        }

        public async Task<ConversationResponseDto> GetUserConversationPagedAsync(int userId, ConversationRequestDto request)
        {
            var (messages, totalCount) = await _aiAdvicesLogsRepository.GetConversationPaged(
                userId,
                request.userPlantId,
                request.plantId,
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

            var logsWithUserPlant = logs
                .Where(l => l.userPlantId != null)
                .GroupBy(l => l.userPlantId)
                .Select(g => g.OrderByDescending(l => l.createdAt).First())
                .Select(log => new LastMessageDto
                {
                    id = log.id,
                    userPlantId = log.userPlantId,
                    plantId = log.plantId,
                    plantName = log.userPlant?.plant?.scientificName ?? log.plant?.scientificName,
                    content = log.content != null && log.content.Length > 100
                                ? log.content.Substring(0, 100)
                                : log.content,
                    createdAt = log.createdAt
                });

            var logsWithStorePlant = logs
                .Where(l => l.userPlantId == null && l.plantId != null)
                .GroupBy(l => l.plantId)
                .Select(g => g.OrderByDescending(l => l.createdAt).First())
                .Select(log => new LastMessageDto
                {
                    id = log.id,
                    userPlantId = null,
                    plantId = log.plantId,
                    plantName = log.plant?.scientificName,
                    content = log.content != null && log.content.Length > 100
                                ? log.content.Substring(0, 100)
                                : log.content,
                    createdAt = log.createdAt
                });

            var logsFree = logs
                .Where(l => l.userPlantId == null && l.plantId == null)
                .OrderByDescending(l => l.createdAt)
                .Take(1)
                .Select(log => new LastMessageDto
                {
                    id = log.id,
                    userPlantId = null,
                    plantId = null,
                    plantName = null,
                    content = log.content != null && log.content.Length > 100
                                ? log.content.Substring(0, 100)
                                : log.content,
                    createdAt = log.createdAt
                });

            var result = logsWithUserPlant.Concat(logsWithStorePlant).Concat(logsFree).ToList();
            return result;
        }
    }
}


