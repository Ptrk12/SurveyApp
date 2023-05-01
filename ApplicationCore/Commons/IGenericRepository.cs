using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Commons
{
    public interface IGenericRepository<T, K> where T : IIdentity<K> where K : IComparable<K>
    {
        Task<T?> GetByIdAsync(K id);
        Task<List<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProp);
        Task<T?> Add (T item);
        Task RemoveById(K id);
        void Update (K id, T item);
    }
}
