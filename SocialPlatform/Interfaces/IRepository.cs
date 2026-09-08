using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPlatform.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T entity);
        T? GetById(Guid id);
        IEnumerable<T> GetAll();
        void Delete(Guid id);
    }
}
