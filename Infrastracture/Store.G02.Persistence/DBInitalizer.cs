using Microsoft.EntityFrameworkCore;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Store.G02.Domain.Entity.Product;

namespace Store.G02.Persistence
{
    public class DBInitalizer(StoreDBContext _context) : IDBInitializer
    {
        public async Task InitalizeAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())
            {

                await _context.Database.MigrateAsync();
            }

            if (!_context.ProductBrands.Any())
            {
                //Infrastracture\Store.G02.Persistence\Data\DataSeeding\brands.json
                var BrandsData = await File.ReadAllTextAsync(@"..\Infrastracture\Store.G02.Persistence\Data\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (brands is not null && brands.Count > 0)
                {
                    await _context.ProductBrands.AddRangeAsync(brands);
                }
            }

           if(!_context.ProductTypes.Any())
            {
                //Infrastracture\Store.G02.Persistence\Data\DataSeeding\types.json
                var TypesData = await File.ReadAllTextAsync(@"..\Infrastracture\Store.G02.Persistence\Data\DataSeeding\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                if (types is not null && types.Count > 0)
                {
                    await _context.ProductTypes.AddRangeAsync(types);
                }
            }

           if(!_context.Products.Any())
            {
                //Infrastracture\Store.G02.Persistence\Data\DataSeeding\products.json
                var ProductsData = await File.ReadAllTextAsync(@"..\Infrastracture\Store.G02.Persistence\Data\DataSeeding\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products is not null && products.Count > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                }
            }


            await _context.SaveChangesAsync();


        }

       


    }

}
