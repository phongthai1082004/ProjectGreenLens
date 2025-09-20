namespace ProjectGreenLens.Exceptions
{
    public static class PlantValidationMessages
    {
        // Scientific Name
        public const string RequiredScientificName = "Tên khoa học không được để trống";
        public const string ScientificNameMaxLength = "Tên khoa học không được vượt quá 150 ký tự";

        // Common Name
        public const string CommonNameMaxLength = "Tên thông thường không được vượt quá 150 ký tự";

        // Description
        public const string DescriptionMaxLength = "Mô tả không được vượt quá 2000 ký tự";

        // Care Instructions
        public const string CareInstructionsMaxLength = "Hướng dẫn chăm sóc không được vượt quá 2000 ký tự";

        // Plant Category
        public const string InvalidPlantCategoryId = "ID danh mục cây không hợp lệ";

        // Watering Frequency
        public const string RequiredWateringFrequency = "Tần suất tưới nước không được để trống";
        public const string InvalidWateringFrequency = "Tần suất tưới nước phải là số nguyên dương";

        // Light Requirement
        public const string RequiredLightRequirement = "Yêu cầu ánh sáng không được để trống";
        public const string InvalidLightRequirement = "Yêu cầu ánh sáng phải là số nguyên dương";

        // Soil Type
        public const string SoilTypeMaxLength = "Loại đất không được vượt quá 100 ký tự";

        // Average Price
        public const string InvalidAveragePrice = "Giá trung bình phải là số dương";
    }
}
