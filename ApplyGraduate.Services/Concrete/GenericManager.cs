using System.Linq.Expressions;
using ApplyGraduate.Data.Abstract;
using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Data.Utilities.Results.Concrete;
using ApplyGraduate.Entities.Concrete;
using ApplyGraduate.Services.Abstract;

namespace ApplyGraduate.Services.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : EntityBase, new()
    {
        private readonly IGenericRepository<T> _repository;
        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<IResult> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<IResult> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<IResult> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity.ResultStatus != ResultStatus.SUCCESS)
                return entity;

            entity.Data.IsDeleted = true;
            return await UpdateAsync(entity.Data);
        }

        public async Task<IResult> HardDeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity.ResultStatus != ResultStatus.SUCCESS)
                return entity;

            return await _repository.DeleteAsync(entity.Data);
        }

        public async Task<DataResult<T>> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            return await _repository.GetByIdAsync(e => e.Id == id, includeProperties);
        }

        public async Task<DataResult<T>> GetAllByNonDeletedAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            return await GetAllAsync(e => e.IsDeleted == false, includeProperties);
        }

        public async Task<DataResult<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            if (predicate != null)
            {
                return await _repository.GetListAsync(predicate, includeProperties);
            }

            return await _repository.GetListAsync(includeProperties: includeProperties);
        }

        public async Task<DataResult<T>> GetAllAsync(IQueryable<T> query)
        {

            return await _repository.GetListAsync(query);
        }

        public IQueryable<T> SetQuery()
        {
            return _repository.SetQuery();
        }
    }
}