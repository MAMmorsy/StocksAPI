using StocksAPI.CORE.Helpers;
using StocksAPI.CORE.Interfaces.Repositories.Shared;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.EF.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using Z.EntityFramework.Plus;

namespace StocksAPI.EF.Repositories.Shared
{
    public class BaseRepository<T> : IContextStockRepository<T> where T : class
    {
        //protected STEMDbContext _context;
        protected DbContext _context;
        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return query.SingleOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var incluse in includes)
                    query = query.Include(incluse);

            return await query.SingleOrDefaultAsync(criteria);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return query.Where(criteria).ToList();
        }
        
        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(criteria).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync<Y>(Expression<Func<T, bool>> criteria, string[] includes = null, Expression<Func<T, IEnumerable<Y>>> includecriteria=null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (includecriteria != null)
                return await query.IncludeFilter(includecriteria).Where(criteria).ToListAsync();
            else
                return await query.Where(criteria).ToListAsync();
        }
        
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int take, int skip)
        {
            return await _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, int? take, int? skip,
            Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>().Where(criteria);

            if (take.HasValue)
                query = query.Take(take.Value);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return await query.ToListAsync();
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public T Update(bool exclude, T oTEntity, params Expression<Func<T, object>>[] updatedProperties)
        {
            _context.Set<T>().Attach(oTEntity);
            if (updatedProperties != null && updatedProperties.Any())
            {
                if (exclude)
                {
                    _context.Entry(oTEntity).State = EntityState.Modified;
                }
                foreach (var property in updatedProperties)
                {
                    _context.Entry(oTEntity).Property(property).IsModified = !exclude;
                }
            }
            else
            {
                _context.Entry(oTEntity).State = EntityState.Modified;
            }
            return oTEntity;
        }

        public bool Update(T oTEntity, bool exclude = false, params Expression<Func<T, object>>[] updatedProperties)
        {
            _context.Set<T>().Attach(oTEntity);
            if (updatedProperties != null && updatedProperties.Any())
            {
                if (exclude)
                {
                    _context.Entry(oTEntity).State = EntityState.Modified;
                }
                foreach (var property in updatedProperties)
                {
                    _context.Entry(oTEntity).Property(property).IsModified = !exclude;
                }
            }
            else
            {
                _context.Entry(oTEntity).State = EntityState.Modified;
            }
            return true;
        }


        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AttachRange(entities);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(Expression<Func<T, bool>> criteria)
        {
            return _context.Set<T>().Count(criteria);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }


        public IQueryable<T> GetAsQueryable(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true)
        {
            IQueryable<T> queryable = _context.Set<T>().AsQueryable();

            if (expression != null)
            {
                queryable = queryable.Where(expression);
            }

            if (excludeDeleted)
            {
                // Add logic to exclude deleted entities, if applicable
            }

            return queryable;
        }

        public IEnumerable<T> GetAsEnumerable(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true)
        {
            IEnumerable<T> iQueryable;
            iQueryable = GetAsQueryable(expression, excludeDeleted).AsEnumerable();
            return iQueryable ?? null;
        }

        public bool Delete(Expression<Func<T, bool>> expression)
        {
            List<T> lTEntity = _context.Set<T>().Where(expression).ToList();
            if (lTEntity.Count > 0)
            {
                _context.Set<T>().RemoveRange(lTEntity);
                return true;
            }
            return false;
        }

        public static T CreateNewInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        public bool All(Expression<Func<T, bool>> expression, bool excludeDeleted = true)
        {
            bool isOk = GetAsQueryable(null, excludeDeleted).All(expression);
            return isOk;
        }

        public bool Any(Expression<Func<T, bool>> expression, bool excludeDeleted = true)
        {
            bool isOk = GetAsQueryable(null, excludeDeleted).Any(expression);
            return isOk;
        }
        public T First(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true)
        {
            T oTEntity = CreateNewInstance<T>();
            oTEntity = GetAsQueryable(expression).FirstOrDefault();

            return oTEntity ?? null;

        }
        public T Last(Expression<Func<T, bool>> expression = null, bool excludeDeleted = true)
        {
            T oTEntity = CreateNewInstance<T>();
            oTEntity = GetAsQueryable(expression).Last();
            return oTEntity ?? null;

        }


    }
}
