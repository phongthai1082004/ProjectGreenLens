using ProjectGreenLens.Models.DTOs.Plant;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Services.Interfaces
{
    public interface IPlantService : IBaseService<Plant, PlantResponseDto, PlantAddDto, PlantUpdateDto>
    {
        // Main query method - replaces all other query methods
        Task<PagedResult<PlantResponseDto>> GetPlantsAsync(PlantQueryDto query);

        // Get single plant with details
        Task<PlantResponseDto> GetByIdWithDetailsAsync(int id);
    }
}
