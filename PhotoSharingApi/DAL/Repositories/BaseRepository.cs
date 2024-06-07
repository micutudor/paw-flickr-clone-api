using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Models;
using PhotoSharingApi.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PhotoSharingApi.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public virtual async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual async Task Update(Expression<Func<T, bool>> filter, Action<T> updateAction)
        {
            var entity = await _dbSet.Where(filter).FirstOrDefaultAsync();

            if (entity != null)
            {
                _dbSet.Entry(entity).State = EntityState.Modified;
                updateAction(entity);
            }

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task Delete(Expression<Func<T, bool>> filter)
        {
            var entities = _dbSet.Where(filter).ToList();

            foreach (var entity in entities) 
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
