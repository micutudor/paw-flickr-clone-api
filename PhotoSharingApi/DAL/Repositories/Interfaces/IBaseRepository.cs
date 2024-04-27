using System.Runtime.InteropServices;

namespace PhotoSharingApi.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        List<T> GetAll();
        void Delete(T entity);
    }
}
