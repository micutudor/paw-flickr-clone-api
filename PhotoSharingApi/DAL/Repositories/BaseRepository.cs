using Microsoft.EntityFrameworkCore;
using PhotoSharingApi.DAL.Repositories.Interfaces;

namespace PhotoSharingApi.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DataContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
