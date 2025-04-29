using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.OrderEntites;

namespace Services.Specifications
{
    internal class OrderWithPaymentintentSpecifications:Specifications<OrderEntity>
    {
        public OrderWithPaymentintentSpecifications( string paymentIntentId):base(O=>O.PaymentIntentId==paymentIntentId)
        {

        }
    }
}
