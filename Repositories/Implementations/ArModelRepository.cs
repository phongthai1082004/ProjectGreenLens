using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class ArModelRepository : BaseRepository<ArModel>, IArModelRepository
    {
        public ArModelRepository(GreenLensDbContext context) : base(context)
        {
        }
    }
}
