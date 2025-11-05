using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity.Product;
using Store.G02.Services.Abstractions.Product;
using Store.G02.Services.Specifications;
using Store.G02.Services.Specifications.Products;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Products
{
    public class ProductService(IUnitOfWork _Unit, IMapper _mapper) : IProductService
    {
        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductParams parameters)
        {
            //var spec = new BaseSpecification<int, Product>(null);
            //spec.Includes.Add(P => P.Type);
            //spec.Includes.Add(P => P.Brand);
            var spec = new ProductsWithBrandsAndTypesSpecification( parameters);

            
          var Products =  await _Unit.GetRepo<int, Product>().GetAllAsync(spec);
            var Result = _mapper.Map<IEnumerable<ProductResponse>>(Products);
            var specCount = new ProductsCountSpecifications(parameters);
            var Count =await _Unit.GetRepo<int, Product>().CountAsync(specCount);

            return new PaginationResponse<ProductResponse>(parameters.PageIndex,parameters.PageSize,Count,Result);
        }
        public async Task<ProductResponse> GetProductByIdAsync(int Id)
        {

            var spec = new ProductsWithBrandsAndTypesSpecification(Id);



            var Product = await _Unit.GetRepo<int, Product>().GetAsync(spec);
            var Result = _mapper.Map<ProductResponse>(Product);

            return Result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var Brands = await _Unit.GetRepo<int, ProductBrand>().GetAllAsync();
            var Result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Brands);

            return Result;
        }

        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var Types = await _Unit.GetRepo<int, ProductType>().GetAllAsync();
            var Result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Types);

            return Result;
        }

    }
}
