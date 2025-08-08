using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int id { get; set; }

        [Required]
        public Guid uniqueGuid { get; set; }

        [Required]
        public DateTime createdAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime updatedAt { get; set; } = DateTime.UtcNow;

        public BaseEntity()
        {
        }
    }
}
