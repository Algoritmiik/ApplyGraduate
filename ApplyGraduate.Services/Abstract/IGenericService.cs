using System.Linq.Expressions;
using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.Concrete;
using ApplyGraduate.Entities.Concrete;

namespace ApplyGraduate.Services.Abstract
{
    public interface IGenericService<T> where T : EntityBase, new()
    {
        Task<IResult> AddAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> DeleteAsync(int id);
        Task<IResult> HardDeleteAsync(int id);
        Task<DataResult<T>> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task<DataResult<T>> GetAllByNonDeletedAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<DataResult<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<DataResult<T>> GetAllAsync(IQueryable<T> query);
        IQueryable<T> SetQuery();
    }
}