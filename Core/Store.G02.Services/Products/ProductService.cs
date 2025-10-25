using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity.Product;
using Store.G02.Services.Abstractions.Product;
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
        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
          var Products =  await _Unit.GetRepo<int, Product>().GetAllAsync();
            var Result = _mapper.Map<IEnumerable<ProductResponse>>(Products);

            return Result;
        }
        public async Task<ProductResponse> GetProductByIdAsync(int Id)
        {
          var Product = await _Unit.GetRepo<int, Product>().GetAsync(Id);
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
