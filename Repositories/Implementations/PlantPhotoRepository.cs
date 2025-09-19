using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class PlantPhotoRepository : BaseRepository<Photo>, IPlantPhotoRepository
    {
        public PlantPhotoRepository(GreenLensDbContext context) : base(context)
        {
        }
    }
}
