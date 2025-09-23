using ProjectGreenLens.Models.DTOs.AIAdvice;

namespace ProjectGreenLens.Models.DTOs.Conversation
{
    public class ConversationResponseDto
    {
        public IEnumerable<AIAdviceResponseDto> messages { get; set; } = new List<AIAdviceResponseDto>();
        public bool hasMore { get; set; }
        public int currentPage { get; set; }
        public int totalCount { get; set; }
        public int totalPages { get; set; }
    }
}
