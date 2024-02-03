using StocksAPI.CORE.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace StocksAPI.CORE.Interfaces.Repositories.Shared
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        Task<T> GetByIdAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int take, int skip);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> FindAllAsync<Y>(Expression<Func<T, bool>> criteria, string[] includes = null, Expression<Func<T, IEnumerable<Y>>> includecriteria = null);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int skip, int take);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);
        T Update(bool exclude, T oTEntity, params Expression<Func<T, object>>[] updatedProperties);
        bool Update(T oTEntity, bool exclude = false, params Expression<Func<T, object>>[] updatedProperties);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> criteria);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
        IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true);
        IEnumerable<T> GetAsEnumerable(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true);
        bool Delete(Expression<Func<T, bool>> expression);
        bool All(Expression<Func<T, bool>> expression, bool excludeDeleted = true);
        bool Any(Expression<Func<T, bool>> expression, bool excludeDeleted = true);

        T First(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true);
        T Last(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true);
    }
}