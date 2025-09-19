using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class PlantCategoryRepository : BaseRepository<PlantCategory>, IPlantCategoryRepository
    {
        public PlantCategoryRepository(GreenLensDbContext context) : base(context)
        {
        }
    }
}
