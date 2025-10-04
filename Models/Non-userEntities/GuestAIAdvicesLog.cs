using ProjectGreenLens.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Non_userEntities
{
    public class GuestAIAdvicesLog : BaseEntity
    {
        [Required]
        public string GuestToken { get; set; } = null!;
        public string Role { get; set; } = "user"; // "user" or "assistant"
        public string Content { get; set; } = null!;
        public int? UserPlantId { get; set; }
    }
}
