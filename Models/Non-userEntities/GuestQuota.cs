namespace ProjectGreenLens.Models.Non_userEntities
{
    using global::ProjectGreenLens.Models.Entities;
    using System;
    using System.ComponentModel.DataAnnotations;

    namespace ProjectGreenLens.Models.Entities
    {
        public class GuestQuota : BaseEntity
        {
            [Key]
            public string GuestToken { get; set; } = null!;
            public int UsedCount { get; set; }
            public DateTime LastUsedAt { get; set; }
        }
    }
}
