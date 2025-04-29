using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class OrderEntity:BaseEntity<Guid>
    {
       
      

        public string UserEmail { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        
        public OrderPaymentStatus PaymentStatus { get; set; }
        public int? DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public OrderEntity() { }
        public OrderEntity(string userEmail, ShippingAddress shippingAddress, IEnumerable<OrderItem> orderItem, DeliveryMethod deliveryMethod, decimal subTotal ,string paymentIntentId)
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItem;
            SubTotal = subTotal;
            DeliveryMethod = deliveryMethod;
            PaymentIntentId = paymentIntentId;
        }
    }
        
    }
