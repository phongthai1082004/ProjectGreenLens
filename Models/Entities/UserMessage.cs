using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class UserMessage : BaseEntity
    {
        [Required]
        public int senderId { get; set; }

        [ForeignKey(nameof(senderId))]
        [InverseProperty(nameof(User.sentMessages))]
        public User sender { get; set; } = null!;

        [Required]
        public int receiverId { get; set; }

        [ForeignKey(nameof(receiverId))]
        [InverseProperty(nameof(User.receivedMessages))]
        public User receiver { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string messageText { get; set; } = null!;

        [Required]
        public bool isRead { get; set; }
    }
}
