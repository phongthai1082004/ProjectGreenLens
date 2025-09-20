using ProjectGreenLens.Models.DTOs.Plant;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IPlantRepository : IBaseRepository<Plant>
    {
        Task<PagedResult<Plant>> GetPlantsAsync(PlantQueryDto query);
        Task<Plant?> GetByIdWithDetailsAsync(int id);
    }
}
