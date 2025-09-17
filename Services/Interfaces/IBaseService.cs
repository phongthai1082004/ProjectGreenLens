namespace ProjectGreenLens.Services.Interfaces
{
    public interface IBaseService<TEntity, TResponseDto, TAddDto, TUpdateDto>
    {
        Task<IEnumerable<TResponseDto>> GetAllAsync();
        Task<TResponseDto> GetByIdAsync(int id);
        Task<TResponseDto> CreateAsync(TAddDto dto);
        Task<TResponseDto> UpdateAsync(TUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
