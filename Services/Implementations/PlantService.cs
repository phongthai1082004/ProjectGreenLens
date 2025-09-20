using AutoMapper;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Models.DTOs.Plant;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Services.Implementations
{
    public class PlantService : BaseService<Plant, PlantResponseDto, PlantAddDto, PlantUpdateDto>, IPlantService
    {
        private readonly IPlantRepository _plantRepository;
        private readonly IMapper _mapper;

        public PlantService(IPlantRepository plantRepository, IMapper mapper)
            : base(plantRepository, mapper)
        {
            _plantRepository = plantRepository;
            _mapper = mapper;
        }


        public async Task<PagedResult<PlantResponseDto>> GetPlantsAsync(PlantQueryDto query)
        {
            // Validate query parameters
            ValidateQuery(query);

            // Call repository to get paged results
            var result = await _plantRepository.GetPlantsAsync(query);

            // Map entities to DTOs
            var mappedItems = _mapper.Map<IEnumerable<PlantResponseDto>>(result.Items);

            // Return paged result with mapped items
            return new PagedResult<PlantResponseDto>
            {
                Items = mappedItems,
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        private void ValidateQuery(PlantQueryDto query)
        {
            if (query.PageNumber < 1)
                throw new ValidationException("Số trang phải lớn hơn 0");

            if (query.PageSize < 1 || query.PageSize > 100)
                throw new ValidationException("Kích thước trang phải từ 1 đến 100");

            if (query.MinPrice.HasValue && query.MinPrice < 0)
                throw new ValidationException("Giá tối thiểu phải lớn hơn hoặc bằng 0");

            if (query.MaxPrice.HasValue && query.MaxPrice < 0)
                throw new ValidationException("Giá tối đa phải lớn hơn hoặc bằng 0");

            if (query.MinPrice.HasValue && query.MaxPrice.HasValue && query.MinPrice > query.MaxPrice)
                throw new ValidationException("Giá tối thiểu không được lớn hơn giá tối đa");

            if (query.WateringFrequency.HasValue && query.WateringFrequency < 1)
                throw new ValidationException("Tần suất tưới nước phải lớn hơn 0");

            if (query.LightRequirement.HasValue && query.LightRequirement < 1)
                throw new ValidationException("Yêu cầu ánh sáng phải lớn hơn 0");

            if (query.PlantCategoryId.HasValue && query.PlantCategoryId < 1)
                throw new ValidationException("ID danh mục cây phải lớn hơn 0");

            // Validate search term length
            if (!string.IsNullOrEmpty(query.SearchTerm) && query.SearchTerm.Trim().Length < 2)
                throw new ValidationException("Từ khóa tìm kiếm phải có ít nhất 2 ký tự");
        }

        public async Task<PlantResponseDto> GetByIdWithDetailsAsync(int id)
        {
            var plant = await _plantRepository.GetByIdWithDetailsAsync(id);
            if (plant == null)
                throw new NotFoundException($"Không tìm thấy cây có ID {id}");

            return _mapper.Map<PlantResponseDto>(plant);
        }

        public override async Task<PlantResponseDto> CreateAsync(PlantAddDto dto)
        {
            // Business validation
            await ValidateCreatePlant(dto);

            return await base.CreateAsync(dto);
        }

        public override async Task<PlantResponseDto> UpdateAsync(PlantUpdateDto dto)
        {
            // Business validation
            await ValidateUpdatePlant(dto);

            return await base.UpdateAsync(dto);
        }


        private async Task ValidateCreatePlant(PlantAddDto dto)
        {
            // Validate PlantCategoryId if provided
            if (dto.PlantCategoryId.HasValue)
            {
                // TODO: Implement category existence check
                //var categoryExists = await _categoryRepository.ExistsAsync(dto.PlantCategoryId.Value);
                //if (!categoryExists)
                //    throw new ValidationException("Danh mục cây không tồn tại");
            }

            // Validate scientific name uniqueness
            var existingPlant = await CheckScientificNameExists(dto.ScientificName);
            if (existingPlant)
                throw new ValidationException($"Tên khoa học '{dto.ScientificName}' đã tồn tại");

            // Additional business rules
            if (dto.WateringFrequency > 30)
                throw new ValidationException("Tần suất tưới nước không được quá 30 ngày");

            if (dto.LightRequirement > 10)
                throw new ValidationException("Yêu cầu ánh sáng không được quá mức 10");

            if (dto.AveragePrice.HasValue && dto.AveragePrice > 10000000) // 10 million
                throw new ValidationException("Giá trung bình không được quá 10,000,000");
        }

        private async Task ValidateUpdatePlant(PlantUpdateDto dto)
        {
            // Validate PlantCategoryId if provided
            if (dto.PlantCategoryId.HasValue)
            {
                // TODO: Implement category existence check
                // var categoryExists = await _categoryRepository.ExistsAsync(dto.PlantCategoryId.Value);
                // if (!categoryExists)
                //     throw new ValidationException("Danh mục cây không tồn tại");
            }

            // Validate scientific name uniqueness (excluding current plant)
            var existingPlant = await CheckScientificNameExists(dto.ScientificName, dto.id);
            if (existingPlant)
                throw new ValidationException($"Tên khoa học '{dto.ScientificName}' đã tồn tại");

            // Additional business rules (same as create)
            if (dto.WateringFrequency > 30)
                throw new ValidationException("Tần suất tưới nước không được quá 30 ngày");

            if (dto.LightRequirement > 10)
                throw new ValidationException("Yêu cầu ánh sáng không được quá mức 10");

            if (dto.AveragePrice.HasValue && dto.AveragePrice > 10000000) // 10 million
                throw new ValidationException("Giá trung bình không được quá 10,000,000");
        }

        private async Task<bool> CheckScientificNameExists(string scientificName, int? excludeId = null)
        {
            var query = new PlantQueryDto
            {
                SearchTerm = scientificName,
                PageSize = 1,
                PageNumber = 1,
                IncludeDetails = false
            };

            var result = await _plantRepository.GetPlantsAsync(query);
            var existingPlants = result.Items.Where(p =>
                p.scientificName.Equals(scientificName, StringComparison.OrdinalIgnoreCase));

            if (excludeId.HasValue)
            {
                existingPlants = existingPlants.Where(p => p.id != excludeId.Value);
            }

            return existingPlants.Any();
        }

    }
}
