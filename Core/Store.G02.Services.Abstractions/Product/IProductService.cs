using Store.G02.Shared.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Product
{
    public interface IProductService
    {
       Task<IEnumerable<ProductResponse>> GetAllProductsAsync(int? BrandId, int? TypeId,string? Sort,string search,int? PageIndex,int? PageSize);

       Task<ProductResponse> GetProductByIdAsync(int Id);

        Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();

        Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync();



    }
}
