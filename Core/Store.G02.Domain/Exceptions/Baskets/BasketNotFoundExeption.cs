using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Exceptions.Baskets
{
    public class BasketNotFoundExeption(string Id) : NotFoundExeption($"Basket With Id : {Id} Was Not Found !!")
    {
    }
}
