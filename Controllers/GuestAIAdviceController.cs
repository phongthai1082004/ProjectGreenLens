using Microsoft.AspNetCore.Mvc;
using ProjectGreenLens.Models.DTOs.GuestAIAdvice;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Controllers
{
    [ApiController]
    [Route("api/guest-ai-advice")]
    public class GuestAIAdviceController : ControllerBase
    {
        private readonly IGuestAIAdviceService _adviceService;

        public GuestAIAdviceController(IGuestAIAdviceService adviceService)
        {
            _adviceService = adviceService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] GuestAIAdviceRequestDto request)
        {
            var response = await _adviceService.GetAdviceAsync(request);
            return Ok(ApiResponse<GuestAIAdviceResponseDto>.Ok(response, "Lấy lời khuyên thành công!"));
        }

        [HttpGet("conversation-paged")]
        public async Task<IActionResult> GetConversationPaged([FromQuery] GuestAIAdvicePagedRequestDto request)
        {
            var response = await _adviceService.GetConversationPagedAsync(request.GuestToken, request);
            return Ok(ApiResponse<GuestConversationPagedResponseDto>.Ok(response, "Lấy lời khuyên thành công!"));
        }
    }
}
