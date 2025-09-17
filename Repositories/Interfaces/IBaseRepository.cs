namespace ProjectGreenLens.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task DeleteAsync(T entity);
    }
}
