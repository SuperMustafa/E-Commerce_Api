using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Basket;

namespace Services.Abstraction.Payment
{
    public interface IPaymentService
    {
        public  Task<BasketDto> CreateOrUpdatePaymentAsync(string basketId);
        public Task UpdateOrderPaymentStatus(string request, string stripeHeader);
    }
}
