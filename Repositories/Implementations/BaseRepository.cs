using Microsoft.EntityFrameworkCore;
using ProjectGreenLens.Infrastructure.dbContext;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;

namespace ProjectGreenLens.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly GreenLensDbContext _context;

        public BaseRepository(GreenLensDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().Where(e => !e.isDelete).ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            entity.uniqueGuid = Guid.NewGuid();
            entity.createdAt = DateTime.UtcNow;
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.updatedAt = DateTime.UtcNow;
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.id == id && !e.isDelete);
        }

        public async Task DeleteAsync(T entity)
        {
            entity.isDelete = true;
            entity.deletedAt = DateTime.UtcNow;
            await UpdateAsync(entity);
        }
    }
}
