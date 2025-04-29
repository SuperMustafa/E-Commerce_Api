using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Order;

namespace Services.Abstraction.Order
{
    public interface IOrderService
    {
        public Task<OrderResultDto> GetOrderByIdAsync(Guid id);

        public Task<IEnumerable<OrderResultDto>> GetOrdersByEmailAsync(string email);
        public Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto,string UserEmail);

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();
    }
}
