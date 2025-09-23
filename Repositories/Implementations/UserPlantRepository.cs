using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class UserPlantRepository : BaseRepository<UserPlant>, IUserPlantRepository
    {
        public UserPlantRepository(GreenLensDbContext context) : base(context)
        {
        }

        public async Task<UserPlant?> GetUserPlantWithDetails(int plantId)
        {
            return await _context.UserPlants.Include(u => u.plant).FirstOrDefaultAsync(u => u.plantId == plantId);
        }
    }
}
