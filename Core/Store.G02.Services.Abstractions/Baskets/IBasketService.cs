using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Baskets
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketByIdAsync(string Id);

        Task<BasketDto?> CrateOrUpdateBasketByIdAsync(BasketDto dto,TimeSpan Duration);

        Task<bool> DelBasketAsync(string Id);


    }
}
