using Store.G02.Services.Abstractions.Auth;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cahce;
using Store.G02.Services.Abstractions.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store.G02.Services.Abstractions
{
    public interface IServiceManager
    {
         IProductService ProductService { get; }

         IBasketService basketService { get; }

        ICacheService CacheService { get; }
        IAuthService AuthService { get; }

        

    }
}
