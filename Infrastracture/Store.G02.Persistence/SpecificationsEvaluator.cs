using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class SpecificationsEvaluator
    {
            // _context.Products.Include(P => P.Type).Include(P => P.Brand ).ToListAsync() as IEnumerable<TEntity>
        
        public static IQueryable<TEntity> GetQuery<TKey,TEntity>(IQueryable<TEntity> InputQuery,ISpecifications<TKey,TEntity> spec) where TEntity : BaseEntity<TKey>
        {

            var Query = InputQuery; // _context.Products

            if (spec.Criteria is not null) {

                Query = Query.Where(spec.Criteria);  // _context.Products.Where()
            }

            if (spec.OrderBy is not null) 
            {
                Query = Query.OrderBy(spec.OrderBy);
            }else if(spec.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(spec.OrderByDescending);
            }

            // _context.Products.Include(P => P.Type).Include(P => P.Brand )
            Query =  spec.Includes.Aggregate(Query, (Query, IncludeExpression) => Query.Include(IncludeExpression)); 

            return Query;
            
            
        }
    }
}
