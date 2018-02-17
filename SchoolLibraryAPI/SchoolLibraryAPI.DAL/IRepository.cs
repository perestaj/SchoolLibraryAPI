using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolLibraryAPI.DAL
{
    public interface IRepository<T>
    {
        int Add(T entity);
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }
}
