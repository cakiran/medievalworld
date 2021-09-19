using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public interface IGenericRepository
    {
        Task<TResult[]> FindAllWithFilterAsync<TEntity,TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector) where TEntity : class;
        Task<TResult> SingleOrDefaultAsync<TEntity,TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity,TResult>> selector) where TEntity : class;
        Task<TEntity> SingleOrDefaultAsync<TEntity>( Expression<Func<TEntity, bool>> selector) where TEntity : class;
        Task Add<TEntity>(TEntity entity) where TEntity : class;
        Task AddRangeAsync<TEntity>(TEntity[] entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void UpdateRange<TEntity>( TEntity[] entities) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        Task ExecuteSqlAsync(string sql);
        Task<TResult[]> FindAllAsync<TEntity, TResult>(Expression<Func<TEntity, TResult>> selector) where TEntity : class;
    }

    public abstract class GenericRepository<TContext> where TContext : DbContext, new()
    {
        public virtual TContext GetContext()
        {
            return new TContext();
        }
        public async Task Add<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = GetContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync<TEntity>(TEntity[] entity) where TEntity : class
        {
            using (var context = GetContext())
            {
                await context.Set<TEntity>().AddRangeAsync(entity);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = GetContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TResult[]> FindAllWithFilterAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector) where TEntity : class
        {
            using (var context = GetContext())
            {
                return await context.Set<TEntity>().AsNoTracking().Where(expression).Select(selector).ToArrayAsync().ConfigureAwait(false);
            }
        }

        public async Task<TResult[]> FindAllAsync<TEntity, TResult>( Expression<Func<TEntity, TResult>> selector) where TEntity : class
        {
            using (var context = GetContext())
            {
                return await context.Set<TEntity>().AsNoTracking().Select(selector).ToArrayAsync().ConfigureAwait(false);
            }
        }

        public async Task<TResult> SingleOrDefaultAsync<TEntity,TResult>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TResult>> selector) where TEntity : class
        {
            using (var context = GetContext())
            {
                return await context.Set<TEntity>().AsNoTracking().Where(expression).Select(selector).SingleOrDefaultAsync().ConfigureAwait(false);
            }
        }

        public async Task<TEntity> SingleOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class
        {
            using (var context = GetContext())
            {
                return await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(expression).ConfigureAwait(false);
            }
        }

        public async Task UpdateChanges<TEntity>(TEntity[] entities) where TEntity : class
        {
            using (var context = GetContext())
            {
                context.Set<TEntity>().UpdateRange(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateChange<TEntity>(TEntity entity) where TEntity : class
        {
            using (var context = GetContext())
            {
                context.Set<TEntity>().Update(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task ExecuteSqlAsync(string sql)
        {
            using (var context = GetContext())
            {
                await context.Database.ExecuteSqlRawAsync(sql);
                await context.SaveChangesAsync();
            }
        }
    }
}
