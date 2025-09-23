namespace ProjectGreenLens.Models.DTOs.Conversation
{
    public class ConversationRequestDto
    {
        public int? userPlantId { get; set; }
        public int pageSize { get; set; } = 30;
        public int page { get; set; } = 1;
    }
}
