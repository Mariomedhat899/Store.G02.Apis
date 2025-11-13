using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface ICacheRepo
    {
        Task<string?> GetDataAsync(string key);

        Task SetDataAsync(string key,Object Value ,TimeSpan duration);

    }
}
