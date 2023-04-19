using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commons
{
    public interface IGenericRepository<T, K> : IIdentity<K> where T : class where K :IComparable<K>
    {
        Task<T?> GetByIdAsync(K id);
        Task<List<T>> GetAllAsync();
        T Add (T item);
        void RemoveById(K id);
        void Update (K id, T item);
    }
}
