using AutoMapper;
using ProjectGreenLens.Models.Entities;
using ProjectGreenLens.Repositories.Interfaces;
using ProjectGreenLens.Services.Interfaces;

namespace ProjectGreenLens.Services.Implementations
{
    public class BaseService<TEntity, TResponseDto, TAddDto, TUpdateDto>
    : IBaseService<TEntity, TResponseDto, TAddDto, TUpdateDto>
    where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        public async Task<TResponseDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with Id={id} was not found.");

            return _mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<TResponseDto> CreateAsync(TAddDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            var created = await _repository.CreateAsync(entity);
            return _mapper.Map<TResponseDto>(created);
        }

        public virtual async Task<TResponseDto> UpdateAsync(TUpdateDto dto)
        {

            var existing = await _repository.GetByIdAsync((dto as dynamic).id);
            if (existing == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with Id={(dto as dynamic).id} was not found.");


            _mapper.Map(dto, existing);

            await _repository.UpdateAsync(existing);
            return _mapper.Map<TResponseDto>(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with Id={id} was not found.");

            await _repository.DeleteAsync(entity);
            return true;
        }
    }
}
