using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectGreenLens.Models.Entities
{
    public class ContactMessage : BaseEntity
    {
        public int? userId { get; set; }
        [ForeignKey(nameof(userId))]
        public User? user { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string email { get; set; } = null!;

        [Required, MaxLength(200)]
        public string subject { get; set; } = null!;

        [Required, MaxLength(2000)]
        public string content { get; set; } = null!;

        public bool isResolved { get; set; } = false;
    }
}
