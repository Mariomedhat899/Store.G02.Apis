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

        public ProductsWithBrandsAndTypesSpecification(int? BrandId, int? TypeId, string? Sort) : base
            (
            P =>
            (!BrandId.HasValue || P.BrandId == BrandId)
            &&
             (!TypeId.HasValue || P.TypeId == TypeId)
            )
        {

            ApplySorting(Sort);
            ApplyIncludes();

        }

        private void ApplySorting(string? Sort)
        {

            if (!string.IsNullOrEmpty(Sort))
            {
                switch (Sort.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;

                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }


        }
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Type);

            Includes.Add(P => P.Brand);
        }
    }
}
