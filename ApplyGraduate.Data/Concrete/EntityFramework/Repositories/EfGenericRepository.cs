using System.Linq.Expressions;
using ApplyGraduate.Data.Abstract;
using ApplyGraduate.Data.Concrete.EntityFramework.Contexts;
using ApplyGraduate.Data.Utilities.Results.Abstract;
using ApplyGraduate.Data.Utilities.Results.ComplexTypes;
using ApplyGraduate.Data.Utilities.Results.Concrete;
using ApplyGraduate.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace ApplyGraduate.Data.Concrete.EntityFramework.Repositories
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : EntityBase, new()
    {
        private readonly ApplyGraduateContext _context;
        public EfGenericRepository(ApplyGraduateContext context)
        {
            _context = context;
        }
        public async Task<IResult> AddAsync(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return new Result(ResultStatus.SUCCESS, "başarıyla eklendi!");
            }
            catch (System.Exception exception)
            {
                return new Result(ResultStatus.ERROR, "eklenirken bir hatayla karşılaşıldı!", exception);
            }
        }

        public async Task<IResult> UpdateAsync(T entity)
        {
            try
            {
                await Task.Run(()=> { _context.Set<T>().Update(entity); });
                await _context.SaveChangesAsync();
                return new Result(ResultStatus.SUCCESS, "başarıyla güncellendi!");
            }
            catch (System.Exception exception)
            {
                return new Result(ResultStatus.ERROR, "güncellenirken bir hatayla karşılaşıldı!", exception);
            }
        }
        public async Task<IResult> DeleteAsync(T entity)
        {
            try
            {
                await Task.Run(() => { _context.Set<T>().Remove(entity); });
                await _context.SaveChangesAsync();
                return new Result(ResultStatus.SUCCESS, "başarıyla silindi!");
            }
            catch (System.Exception exception)
            {
                return new Result(ResultStatus.ERROR, "silinirken bir hatayla karşılaşıldı!", exception);
            }
        }

        public async Task<DataResult<T>> GetByIdAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();
                query = query.Where(predicate);
                
                if(includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }
                
                var data = await query.SingleOrDefaultAsync();
                if (data == null)
                {
                    return new DataResult<T>(ResultStatus.WARNING, "Böyle bir data bulunamadı!", new T());
                }
                return new DataResult<T>(ResultStatus.SUCCESS, data);
            }
            catch (System.Exception exception)
            {
                return new DataResult<T>(ResultStatus.ERROR, "Data getirilirken bir hatayla karşılaşıldı!", new T(), exception);
            }
        }

        public async Task<DataResult<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if(predicate != null)
                    query = query.Where(predicate);
                
                
                if(includeProperties.Any())
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query = query.Include(includeProperty);
                    }
                }

                var datas = await query.ToListAsync();
                if (datas.Count < 0)
                {
                    return new DataResult<T>(ResultStatus.WARNING, "Böyle bir data bulunamadı!", new List<T>());

                }
                return new DataResult<T>(ResultStatus.SUCCESS, datas);
            }
            catch (System.Exception exception)
            {
                
                return new DataResult<T>(ResultStatus.ERROR, "Datalar getirilirken bir hatayla karşılaşıldı!", new List<T>(), exception);
            }
        }

        public async Task<DataResult<T>> GetListAsync(IQueryable<T> query)
        {
            try
            {
                var datas = await query.ToListAsync();
                if (datas.Count < 0)
                {
                    return new DataResult<T>(ResultStatus.WARNING, "Böyle bir data bulunamadı!", new List<T>());

                }
                return new DataResult<T>(ResultStatus.SUCCESS, datas);
            }
            catch (System.Exception exception)
            {
                
                return new DataResult<T>(ResultStatus.ERROR, "Datalar getirilirken bir hatayla karşılaşıldı!", new List<T>(), exception);
            }
        }

        public IQueryable<T> SetQuery()
        {
            return _context.Set<T>();
        }
    }
}