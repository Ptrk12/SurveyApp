using ApplicationCore.Commons;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class, IIdentity<K> where K : IComparable<K>
    {
        public K Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T Add(T item)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetByIdAsync(K id)
        {
            throw new NotImplementedException();
        }

        public void RemoveById(K id)
        {
            throw new NotImplementedException();
        }

        public void Update(K id, T item)
        {
            throw new NotImplementedException();
        }
    }
}
