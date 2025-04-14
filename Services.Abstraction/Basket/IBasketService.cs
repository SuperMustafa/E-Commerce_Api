using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Basket;

namespace Services.Abstraction.Basket
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string id);
        Task<bool> DeleteBasketAsync(string id);
        Task<BasketDto?> UpdateBasketAsync(BasketDto basket);
    }
}
