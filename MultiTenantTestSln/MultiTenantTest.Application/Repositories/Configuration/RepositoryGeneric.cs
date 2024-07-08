using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MultiTenantTest.Application.Repositories.Configuration
{
    public class RepositoryGeneric<TEntity, TContext> : IRepositoryGeneric<TEntity> where TEntity : class where TContext : DbContext
    {
        public virtual TContext Context { get; protected set; }

        public RepositoryGeneric(TContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> All()
        {
            return Context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return All().Where(predicate);
        }

        public virtual IQueryable<TResult> Transform<TResult>(Expression<Func<TEntity, TResult>> predicate)
        {
            return All().Select(predicate);
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return Filter(predicate).First();
        }

        public virtual TEntity Find(params object[] keys)
        {
            return ((DbSet<TEntity>)All()).Find(keys);
        }

        public virtual ValueTask<TEntity> FindAsync(params object[] keys)
        {
            return FindAsync(CancellationToken.None, keys);
        }

        public virtual ValueTask<TEntity> FindAsync(CancellationToken token, params object[] keys)
        {
            return ((DbSet<TEntity>)All()).FindAsync(keys, token);
        }

        public virtual Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Filter(predicate).FirstAsync();
        }

        public TEntity Create()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Create(TEntity entity)
        {
            Context.Add(entity);
            return entity;
        }

        public virtual async Task CreateRangeAsync(List<TEntity> listEntity)
        {
            await Context.AddRangeAsync(listEntity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return entity;
        }

        public virtual TEntity Update(TEntity oldEntity, TEntity newEntity)
        {
            try
            {
                Context.Set<TEntity>().Attach(newEntity);
                Context.Entry(newEntity).State = EntityState.Detached;
                Context.Set<TEntity>().Attach(oldEntity);
                Context.Entry(oldEntity).State = EntityState.Modified;
                Context.SaveChanges();
                return newEntity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public virtual int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return All().Where(predicate).FirstOrDefault();
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return All().Where(predicate).FirstOrDefaultAsync();
        }

        public void SetOriginalValue<TValue>(TEntity entity, string propertyName, TValue value)
        {
            Context.Entry(entity).OriginalValues[propertyName] = value;
        }
    }
}
