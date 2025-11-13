using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Cahce
{
    public interface ICacheService
    {
      Task  SetDataAsync(string key, object Value, TimeSpan Duration);

        Task<string?> GetDataAsync(string key);

    }
}
