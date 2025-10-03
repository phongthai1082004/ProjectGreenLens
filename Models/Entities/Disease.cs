using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.Entities
{
    public class Disease : BaseEntity
    {
        [Required, MaxLength(100)]
        public string name { get; set; } = null!;

        [MaxLength(1000)]
        public string? symptoms { get; set; }

        [MaxLength(1000)]
        public string? treatment { get; set; }

        [MaxLength(1000)]
        public string? prevention { get; set; }
        public List<UserPlantDisease> userPlantDiseases { get; set; } = new();
    }
}
