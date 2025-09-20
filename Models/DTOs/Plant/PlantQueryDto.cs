using System.ComponentModel.DataAnnotations;

namespace ProjectGreenLens.Models.DTOs.Plant
{
    public class PlantQueryDto
    {
        // Pagination
        [Range(1, int.MaxValue, ErrorMessage = "Số trang phải lớn hơn 0")]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        // Search
        public string? SearchTerm { get; set; }

        // Filters
        public int? PlantCategoryId { get; set; }
        public bool? IsIndoor { get; set; }
        public int? WateringFrequency { get; set; }
        public int? LightRequirement { get; set; }
        public string? SoilType { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        // Sorting
        public PlantSortBy SortBy { get; set; } = PlantSortBy.CreatedAt;
        public SortDirection SortDirection { get; set; } = SortDirection.Descending;

        // Include navigation properties
        public bool IncludeDetails { get; set; } = false;
    }

    public enum PlantSortBy
    {
        Id,
        ScientificName,
        CommonName,
        CreatedAt,
        AveragePrice,
        WateringFrequency,
        LightRequirement,
        CategoryName
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
