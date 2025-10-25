using Store.G02.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepo<Tkey, TEntity> GetRepo<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>;

        Task<int> SaveChangesAsync();

    }
}
