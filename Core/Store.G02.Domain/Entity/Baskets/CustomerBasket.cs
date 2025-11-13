using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.G02.Domain.Entity.Product;

namespace Store.G02.Domain.Entity.Baskets
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public IEnumerable<BasketItem> Items { get; set; }
    }
}
