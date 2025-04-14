using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //GetBasket
        Task<CustomerBasket?>GetBasketAsync(string id);
        //DeleteBasket

        Task<bool>DeleteBasketAsync(string id);

        //CreateOrUpdateBasket
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null);
    }
}
