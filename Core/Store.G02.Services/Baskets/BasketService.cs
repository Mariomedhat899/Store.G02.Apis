using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity.Baskets;
using Store.G02.Domain.Exceptions.Baskets;
using Store.G02.Services.Abstractions.Baskets;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.G02.Domain.Exceptions.BadRequest;

namespace Store.G02.Services.Baskets
{
    public class BasketService(IBasketRepo _basketRepo,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDto?> GetBasketByIdAsync(string Id)
        {
          var Basket = await _basketRepo.GetBasketAsync(Id);

            if (Basket is null) throw new BasketNotFoundExeption(Id);

          var Result =  _mapper.Map<BasketDto>(Basket);

            return Result;
        }
        public async Task<BasketDto?> CrateOrUpdateBasketByIdAsync(BasketDto dto, TimeSpan Duration)
        {
           var basket = _mapper.Map<CustomerBasket>(dto);
           var Result = await  _basketRepo.CreateOrUpdateBasketAsync(basket, Duration);

            if (Result is null) throw new CreateOrUpdateBasketBadRequest();

            return _mapper.Map<BasketDto>(Result);

        }

        public async Task<bool> DelBasketAsync(string Id)
        {
          var Flag = await  _basketRepo.DelBasketAsync(Id);

            if (!Flag) throw new DeleteBasketBadRequest();

            return Flag;
        }

    }
}
