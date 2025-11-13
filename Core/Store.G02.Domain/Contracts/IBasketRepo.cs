using Store.G02.Domain.Entity.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface IBasketRepo
    {

      Task<CustomerBasket?>  GetBasketAsync(string Id);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket,TimeSpan Duration);

        Task<bool> DelBasketAsync(string Id);


    }
}
