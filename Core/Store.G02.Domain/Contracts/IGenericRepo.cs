using Store.G02.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IGenericRepo<TKey,TEntity> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool ChangeTracker = false);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TKey,TEntity> spec, bool ChangeTracker = false);


        Task<TEntity?> GetAsync(TKey key);
        Task<TEntity?> GetAsync(ISpecifications<TKey,TEntity> spec);



        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);


    }
}
