using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class GuideRepository : BaseRepository<Guide>, IGuideRepository
    {
        public GuideRepository(GreenLensDbContext context) : base(context)
        {
        }
    }
}
