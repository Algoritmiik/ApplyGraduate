using System.Linq.Expressions;
using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.Concrete;
using ApplyGraduate.Entities.Concrete;

namespace ApplyGraduate.Data.Abstract
{
    public interface IGenericRepository<T> where T : EntityBase, new()
    {
        Task<IResult> AddAsync(T entity);
        Task<IResult> UpdateAsync(T entity);
        Task<IResult> DeleteAsync(T entity);
        Task<DataResult<T>> GetByIdAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<DataResult<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<DataResult<T>> GetListAsync(IQueryable<T> query);
        IQueryable<T> SetQuery();
    }
}