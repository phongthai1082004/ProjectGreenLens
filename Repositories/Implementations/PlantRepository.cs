using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class PlantRepository : BaseRepository<Plant>, IPLantRepository
    {
        public PlantRepository(GreenLensDbContext context) : base(context)
        {
        }
    }
}
