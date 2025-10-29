using Store.G02.Domain.Entity.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Products
{
    public class ProductsWithBrandsAndTypesSpecification : BaseSpecification<int,Product>
    {
        public ProductsWithBrandsAndTypesSpecification(int Id) : base(P => P.Id == Id)
        {
            ApplyIncludes();



        }

        public ProductsWithBrandsAndTypesSpecification() : base(null)
        {
            ApplyIncludes();

        }


        private void ApplyIncludes()
        {
            Includes.Add(P => P.Type);

            Includes.Add(P => P.Brand);
        }
    }
}
