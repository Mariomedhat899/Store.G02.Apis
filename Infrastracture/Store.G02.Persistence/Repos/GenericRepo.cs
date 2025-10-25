using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repos
{
    public class GenericRepo<TKey, TEntity>(StoreDBContext _context) : IGenericRepo<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false)
        {
            return ChangeTracker ? await _context.Set<TEntity>().ToListAsync()
                                 :await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            return await _context.Set<TEntity>().FindAsync(key);
        }

        public async Task AddAsync(TEntity entity)
        {
          await  _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
           _context.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }



    }
}
