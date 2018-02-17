using System.Collections.Generic;

namespace SchoolLibraryAPI.Core.Services
{
    public interface IService<T>
    {
        IList<T> Get();
        T GetById(int id);
        void Update(T model);
        void Delete(int id);
    }
}
