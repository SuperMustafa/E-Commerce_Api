using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.OrderEntites;

namespace Services.Specifications
{
    public class OrderWithIncludeSpecifications:Specifications<OrderEntity>
    {
        public OrderWithIncludeSpecifications(Guid id) : base(O=>O.Id==id)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);

        }
        public OrderWithIncludeSpecifications(string email) : base(O => O.UserEmail == email)
        {
            AddInclude(O => O.DeliveryMethod);
            AddInclude(O => O.OrderItems);
            SetOrderBy(O => O.OrderDate);

        }
    }
}
