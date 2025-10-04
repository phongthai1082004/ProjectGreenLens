using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Models.DTOs.AIAdvice;
using ProjectGreenLens.Models.DTOs.Conversation;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;
using System.Security.Claims;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIAdviceController : ControllerBase
    {
        private readonly IAIAdviceService _aiAdviceService;

        public AIAdviceController(IAIAdviceService aiAdviceService)
        {
            _aiAdviceService = aiAdviceService;
        }

        [HttpPost("ask")]
        public async Task<ActionResult<ApiResponse<AIAdviceResponseDto>>> GetAdvice([FromBody] AIAdviceRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(ApiResponse<AIAdviceResponseDto>.Fail(GeminiErrorMessages.INVALID_TOKEN));

            // Validate: chỉ được truyền 1 trong 2 trường, không được truyền cả 2
            if (request.userPlantId.HasValue && request.plantId.HasValue)
                return BadRequest(ApiResponse<AIAdviceResponseDto>.Fail("Chỉ được truyền 1 trong 2 trường: userPlantId hoặc plantId."));

            var response = await _aiAdviceService.GetAdviceAsync(request, userId);
            return Ok(ApiResponse<AIAdviceResponseDto>.Ok(response, "Lấy lời khuyên thành công"));
        }

        [HttpGet("conversation")]
        public async Task<ActionResult<ApiResponse<IEnumerable<AIAdviceResponseDto>>>> GetConversation([FromQuery] int? userPlantId = null, [FromQuery] int? plantId = null)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Fail(GeminiErrorMessages.INVALID_TOKEN));

            // Validate: chỉ được truyền 1 trong 2 trường, không được truyền cả 2
            if (userPlantId.HasValue && plantId.HasValue)
                return BadRequest(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Fail("Chỉ được truyền 1 trong 2 trường: userPlantId hoặc plantId."));

            var conversations = await _aiAdviceService.GetUserConversationAsync(userId, userPlantId, plantId);
            return Ok(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Ok(conversations, "Lấy lịch sử hội thoại thành công"));
        }

        [HttpGet("conversation-paged")]
        public async Task<ActionResult<ApiResponse<ConversationResponseDto>>> GetConversationPaged([FromQuery] ConversationRequestDto request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(ApiResponse<ConversationResponseDto>.Fail(GeminiErrorMessages.INVALID_TOKEN));

            // Validate: chỉ được truyền 1 trong 2 trường, không được truyền cả 2
            if (request.userPlantId.HasValue && request.plantId.HasValue)
                return BadRequest(ApiResponse<ConversationResponseDto>.Fail("Chỉ được truyền 1 trong 2 trường: userPlantId hoặc plantId."));

            var result = await _aiAdviceService.GetUserConversationPagedAsync(userId, request);
            return Ok(ApiResponse<ConversationResponseDto>.Ok(result, "Lấy lịch sử hội thoại phân trang thành công"));
        }

        [HttpGet("last-messages")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<IEnumerable<LastMessageDto>>>> GetLastMessagesByUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized(ApiResponse<IEnumerable<LastMessageDto>>.Fail(GeminiErrorMessages.INVALID_TOKEN));

            var result = await _aiAdviceService.GetLastMessagesByUserAsync(userId);

            if (result == null || !result.Any())
                return NotFound(ApiResponse<IEnumerable<LastMessageDto>>.Fail("Không tìm thấy log cho user này."));

            return Ok(ApiResponse<IEnumerable<LastMessageDto>>.Ok(result, "Lấy lịch sử hội thoại mới nhất thành công"));
        }
    }
}
