namespace ProjectGreenLens.Models.DTOs.GuestAIAdvice
{
    public class GuestConversationPagedResponseDto
    {
        public IEnumerable<GuestAIAdviceResponseDto> Messages { get; set; } = new List<GuestAIAdviceResponseDto>();
        public bool HasMore { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
