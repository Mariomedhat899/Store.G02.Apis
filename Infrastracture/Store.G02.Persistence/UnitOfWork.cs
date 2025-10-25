using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Repos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class UnitOfWork(StoreDBContext _Context) : IUnitOfWork
    {

        //private  Dictionary<string, object> _repos = new Dictionary<string, object>();
        private ConcurrentDictionary<string, object> _repos = new ConcurrentDictionary<string, object>();

        //public IGenericRepo<Tkey, TEntity> GetRepo<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_repos.ContainsKey(type))
        //    {
        //        var Repo = new GenericRepo<Tkey, TEntity>(_Context);
        //        _repos.Add(type, Repo);
        //    }

        //    return (IGenericRepo<Tkey, TEntity>) _repos[type];

        //}
        public IGenericRepo<Tkey, TEntity> GetRepo<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        {
          return (IGenericRepo<Tkey, TEntity>) _repos.GetOrAdd(typeof(TEntity).Name, new GenericRepo<Tkey, TEntity>(_Context));
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _Context.SaveChangesAsync();
        }
    }
}
