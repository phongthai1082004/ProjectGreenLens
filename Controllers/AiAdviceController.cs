namespace ProjectGreenLens.Controllers
{
    using global::ProjectGreenLens.Exceptions;
    using global::ProjectGreenLens.Models.DTOs.AIAdvice;
    using global::ProjectGreenLens.Models.DTOs.ArModel;
    using global::ProjectGreenLens.Models.DTOs.Conversation;
    using global::ProjectGreenLens.Services.Interfaces;
    using global::ProjectGreenLens.Settings;
    using Microsoft.AspNetCore.Mvc;
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

            [HttpGet]
            public async Task<ActionResult<ApiResponse<IEnumerable<AIAdviceResponseDto>>>> GetAll()
            {
                var result = await _aiAdviceService.GetAllAsync();
                return Ok(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Ok(result, "Lấy danh sách thành công"));
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<ApiResponse<AIAdviceResponseDto>>> GetById(int id)
            {
                var result = await _aiAdviceService.GetByIdAsync(id);
                return Ok(ApiResponse<AIAdviceResponseDto>.Ok(result, "Lấy thông tin thành công"));
            }

            [HttpPost]
            public async Task<ActionResult<ApiResponse<AIAdviceResponseDto>>> Create([FromBody] AIAdviceAddDto dto)
            {
                var result = await _aiAdviceService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.id },
                    ApiResponse<AIAdviceResponseDto>.Ok(result, "Tạo mới thành công"));
            }

            [HttpPut("{id}")]
            public async Task<ActionResult<ApiResponse<AIAdviceResponseDto>>> Update(int id, [FromBody] AIAdviceUpdateDto dto)
            {
                if (id != dto.id)
                    return BadRequest(ApiResponse<AIAdviceResponseDto>.Fail("Id không khớp"));

                var result = await _aiAdviceService.UpdateAsync(dto);
                return Ok(ApiResponse<AIAdviceResponseDto>.Ok(result, "Cập nhật thành công"));
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult<ApiResponse<bool>>> Delete(int id)
            {
                var result = await _aiAdviceService.DeleteAsync(id);
                return Ok(ApiResponse<bool>.Ok(result, "Xóa thành công"));
            }

            [HttpPost("ask")]
            public async Task<ActionResult<ApiResponse<AIAdviceResponseDto>>> GetAdvice([FromBody] AIAdviceRequestDto request)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(ApiResponse<AIAdviceResponseDto>.Fail(GeminiErrorMessages.INVALID_TOKEN));
                }

                var response = await _aiAdviceService.GetAdviceAsync(request, userId);
                return Ok(ApiResponse<AIAdviceResponseDto>.Ok(response, "Lấy lời khuyên thành công"));
            }

            [HttpGet("conversation")]
            public async Task<ActionResult<ApiResponse<IEnumerable<AIAdviceResponseDto>>>> GetConversation([FromQuery] int? userPlantId = null)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Fail(GeminiErrorMessages.INVALID_TOKEN));
                }

                var conversations = await _aiAdviceService.GetUserConversationAsync(userId, userPlantId);
                return Ok(ApiResponse<IEnumerable<AIAdviceResponseDto>>.Ok(conversations, "Lấy lịch sử hội thoại thành công"));
            }

            [HttpGet("conversation-paged")]
            public async Task<ActionResult<ApiResponse<ConversationResponseDto>>> GetConversationPaged([FromQuery] ConversationRequestDto request)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                {
                    return Unauthorized(ApiResponse<ConversationResponseDto>.Fail(GeminiErrorMessages.INVALID_TOKEN));
                }

                var result = await _aiAdviceService.GetUserConversationPagedAsync(userId, request);
                return Ok(ApiResponse<ConversationResponseDto>.Ok(result, "Lấy lịch sử hội thoại phân trang thành công"));
            }
        }
    }
}
