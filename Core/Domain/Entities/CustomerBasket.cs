using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerBasket // The whole custmor Cart
    {
        public string Id { get; set; }
        public IEnumerable<BasketItem> BasketItems { get; set; }

    }
}
