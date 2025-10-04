namespace ProjectGreenLens.Models.DTOs.GuestAIAdvice
{
    public class GuestAIAdvicePagedRequestDto
    {
        public string GuestToken { get; set; } = null!;
        public int? UserPlantId { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
