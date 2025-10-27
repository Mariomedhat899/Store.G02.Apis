using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity;
using Store.G02.Domain.Entity.Product;
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
            if(typeof(TEntity) == typeof(Product))
            {

            return ChangeTracker ? await _context.Products.Include(P => P.Type).Include(P => P.Brand ).ToListAsync() as IEnumerable<TEntity>
                                 :await _context.Products.Include(P => P.Type).Include(P => P.Brand).ToListAsync() as IEnumerable<TEntity>;
            }
            return ChangeTracker ? await _context.Set<TEntity>().ToListAsync()
                                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        }

        public async Task<TEntity?> GetAsync(TKey key)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                
            return await _context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == key as int?) as TEntity;
            }
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
