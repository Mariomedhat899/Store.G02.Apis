using Store.G02.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface ISpecifications<TKey,TENtity> where TENtity : BaseEntity<TKey>
    {
         List<Expression<Func<TENtity, object>>> Includes { get; set; }
         Expression<Func<TENtity,bool>>? Criteria { get; set; }

        Expression<Func<TENtity,object>>? OrderBy { get; set; }
        Expression<Func<TENtity, object>>? OrderByDescending { get; set; }

         int Skip { get; set; }

         int Take { get; set; }

         bool IsPagination { get; set; }

    }
}
