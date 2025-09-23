using ProjectGreenLens.Models.Entities;

namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IUserPlantRepository : IBaseRepository<UserPlant>
    {
        Task<UserPlant?> GetUserPlantWithDetails(int plantId);
    }
}
