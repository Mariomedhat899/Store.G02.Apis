using StackExchange.Redis;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entity.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repos
{
    public class BasketRepo(IConnectionMultiplexer _connection) : IBasketRepo
    {
            
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var RediusValue = await _database.StringGetAsync(Id);

            if (RediusValue.IsNullOrEmpty) return null;
          

           var Basket =  JsonSerializer.Deserialize<CustomerBasket>(RediusValue);

            if(Basket is null) return null;

            return Basket;
        }
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan Duration)
        {
          var RediusValue =  JsonSerializer.Serialize(basket);

            var Flag = await _database.StringSetAsync(basket.Id,RediusValue,Duration); 

            if(!Flag) return null;

            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DelBasketAsync(string Id)
        {
          return await _database.KeyDeleteAsync(Id);
        }

    }
}
