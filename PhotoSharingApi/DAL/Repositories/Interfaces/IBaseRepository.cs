using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        List<T> GetAll();
        Task Update(Expression<Func<T, bool>> filter, Action<T> updateAction);
        Task Delete(Expression<Func<T, bool>> filter);
    }
}
