using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions.Cahce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Cache
{
    internal class CacheService(ICacheRepo _cacheRepo) : ICacheService
    {
        public async Task<string?> GetDataAsync(string key)
        {
            var Result = await _cacheRepo.GetDataAsync(key);

            return Result;
        }

        public async Task SetDataAsync(string key, object Value, TimeSpan Duration)
        {
            await _cacheRepo.SetDataAsync(key, Value, Duration);
        }
    }
}
