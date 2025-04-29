using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Order
{
    public class OrderResultDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public ShipingAddressDto ShippingAddress { get; set; }
        public ICollection <OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public string  PaymentStatus { get; set; }
      
        public decimal SubTotal { get; set; }
       
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string DeliveryMethod { get; set; }

    }
}
