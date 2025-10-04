using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.DTOs.Plant;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Settings;
using System.Linq.Expressions;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class PlantRepository : BaseRepository<Plant>, IPlantRepository
    {
        public PlantRepository(GreenLensDbContext context) : base(context)
        {
        }

        public async Task<PagedResult<Plant>> GetPlantsAsync(PlantQueryDto query)
        {
            var queryable = _context.Set<Plant>().Where(p => !p.isDelete);

            // Include navigation properties if requested
            if (query.IncludeDetails)
            {
                queryable = queryable
                    .Include(p => p.plantCategory)
                .Include(p => p.photos)
                .Include(p => p.guides);
            }
            else
            {
                queryable = queryable.Include(p => p.plantCategory);
            }

            // Apply filters
            queryable = ApplyFilters(queryable, query);

            // Apply search
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                var searchTerm = query.SearchTerm.Trim().ToLower();
                queryable = queryable.Where(p =>
                    p.scientificName.ToLower().Contains(searchTerm) ||
                    (p.commonName != null && p.commonName.ToLower().Contains(searchTerm)) ||
                    (p.description != null && p.description.ToLower().Contains(searchTerm)) ||
                    (p.plantCategory != null && p.plantCategory.name.ToLower().Contains(searchTerm))
                );
            }

            // Get total count before pagination
            var totalCount = await queryable.CountAsync();

            // Apply sorting
            queryable = ApplySorting(queryable, query);

            // Apply pagination
            var items = await queryable
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResult<Plant>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public async Task<Plant?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Set<Plant>()
                .Where(p => p.id == id && !p.isDelete)
                .Include(p => p.plantCategory)
                .Include(p => p.photos)
                .Include(p => p.guides)
                .FirstOrDefaultAsync();
        }


        private IQueryable<Plant> ApplyFilters(IQueryable<Plant> queryable, PlantQueryDto query)
        {
            if (query.PlantCategoryId.HasValue)
            {
                queryable = queryable.Where(p => p.plantCategoryId == query.PlantCategoryId.Value);
            }

            if (query.IsIndoor.HasValue)
            {
                queryable = queryable.Where(p => p.isIndoor == query.IsIndoor.Value);
            }

            if (query.WateringFrequency.HasValue)
            {
                queryable = queryable.Where(p => p.wateringFrequency == query.WateringFrequency.Value);
            }


            if (!string.IsNullOrWhiteSpace(query.SoilType))
            {
                queryable = queryable.Where(p => p.soilType != null && p.soilType.Contains(query.SoilType));
            }

            if (query.MinPrice.HasValue)
            {
                queryable = queryable.Where(p => p.averagePrice >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                queryable = queryable.Where(p => p.averagePrice <= query.MaxPrice.Value);
            }

            return queryable;
        }

        private IQueryable<Plant> ApplySorting(IQueryable<Plant> queryable, PlantQueryDto query)
        {
            Expression<Func<Plant, object>> sortExpression = query.SortBy switch
            {
                PlantSortBy.Id => p => p.id,
                PlantSortBy.ScientificName => p => p.scientificName,
                PlantSortBy.CommonName => p => p.commonName ?? "",
                PlantSortBy.CreatedAt => p => p.createdAt,
                PlantSortBy.AveragePrice => p => p.averagePrice ?? 0,
                PlantSortBy.WateringFrequency => p => p.wateringFrequency,
                PlantSortBy.CategoryName => p => p.plantCategory != null ? p.plantCategory.name : "",
                _ => p => p.createdAt
            };

            return query.SortDirection == SortDirection.Ascending
                ? queryable.OrderBy(sortExpression)
                : queryable.OrderByDescending(sortExpression);
        }

    }
}
