using Store.G02.Domain.Entity.Product;
using Store.G02.Shared.Dtos.Product;
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

        public ProductsWithBrandsAndTypesSpecification(ProductParams parameters) : base
            (
            P =>
            (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
            &&
             (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
            &&
            ( string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()) )
            )
            
        {

            //PageIndex = 3
            //PageSize = 5
            //Skip = 2  *  5 = 10 (PageIndex - 1) * PageSize
            //Take  = 5

            ApplyPagination(parameters.PageSize, parameters.PageIndex);
            ApplySorting(parameters.Sort);
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
