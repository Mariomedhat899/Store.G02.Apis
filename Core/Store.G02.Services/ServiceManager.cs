using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Services.Abstractions.Cahce;
using Store.G02.Services.Abstractions.Product;
using Store.G02.Services.Baskets;
using Store.G02.Services.Cache;
using Store.G02.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class ServiceManager(
        IUnitOfWork _Unit,
        IMapper _mapper,
        IBasketRepo _basketRepo,
        ICacheRepo _CacheRepo
        ) : IServiceManager
    {
        public IProductService ProductService { get; } = new ProductService(_Unit,_mapper);

        public IBasketService basketService { get; } = new BasketService(_basketRepo, _mapper);

        public ICacheService CacheService { get; } = new CacheService(_CacheRepo);
    }
}
