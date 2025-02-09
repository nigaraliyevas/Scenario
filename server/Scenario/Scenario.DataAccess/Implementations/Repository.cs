using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Repositories;
using Scenario.Core.Entities.Common;
using Scenario.DataAccess.Data;
using System.Linq.Expressions;

namespace MovieApp.DataAccess.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ScenarioAppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(ScenarioAppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Create(T entity)
        {
            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Added;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.ToListAsync() : await query.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);
                }
                return predicate == null ? await query.FirstOrDefaultAsync() : await query.FirstOrDefaultAsync(predicate);

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                return predicate == null ? false : await _table.AnyAsync(predicate);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        public async Task Update(T entity)
        {
            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        private IQueryable<T> GetAllIncludes(params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
                return query;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IQueryable<T> GetAllAsQeuryable(Expression<Func<T, bool>> predicate = null, params string[] includes)
        {
            try
            {
                IQueryable<T> query = _table;


                if (includes.Length > 0)
                {
                    query = GetAllIncludes(includes);

                }

                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                return query;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }

}
