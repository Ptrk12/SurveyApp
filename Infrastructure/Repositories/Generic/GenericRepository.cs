using ApplicationCore.Commons;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class, IIdentity<K> where K : IComparable<K>
    {
        private readonly SurveyDbContext _context;

        public GenericRepository(SurveyDbContext context)
        {
            _context = context;
        }

        public async Task<T?> Add(T item)
        {
            try
            {
                await _context.Set<T>().AddAsync(item);
                return item;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProp)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProp.Aggregate(query, (current, includeProp) => current.Include(includeProp));
            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(K id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task RemoveById(K id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);         
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(K id, T item)
        {
            var foundEntity = _context.Set<T>().Find(id);
            if (foundEntity.Id.CompareTo(item.Id) == 0 && foundEntity != null)
            {
                   _context.Set<T>().Update(item);
            }
        }
    }
}
