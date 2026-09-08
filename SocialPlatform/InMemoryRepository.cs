using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SocialPlatform.Interfaces;

namespace SocialPlatform
{
    public class InMemoryRepository<T> : IRepository<T>
    {
        private readonly List<T> _storage = new List<T>();
        public void Add(T entity)
        {
            _storage.Add(entity);
        }
        public GetById(Guid id)
        {

        }
        public IEnumerable<T> GetAll()
        {
            return _storage;
        }
        public void Delete(Guid id)
        {
            
        }
        
}
