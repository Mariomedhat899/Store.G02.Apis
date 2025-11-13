using StackExchange.Redis;
using Store.G02.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repos
{
    public class CahceRepo(IConnectionMultiplexer _connectionMultiplexer) : ICacheRepo
    {

        private readonly IDatabase _database = _connectionMultiplexer.GetDatabase();
        public async Task<string?> GetDataAsync(string key)
        {
            var Redis = await _database.StringGetAsync(key);

            return Redis;

        }

        public async Task SetDataAsync(string key, object Value, TimeSpan duration)
        {
           await _database.StringSetAsync(key, JsonSerializer.Serialize(Value),duration);
        }
    }
}
