namespace ProjectGreenLens.Models.DTOs.Conversation
{
    public class ConversationRequestDto
    {
        public int? userPlantId { get; set; } // cây user sở hữu
        public int? plantId { get; set; }     // cây cửa hàng (không sở hữu)
        public int pageSize { get; set; } = 30;
        public int page { get; set; } = 1;
    }
}
