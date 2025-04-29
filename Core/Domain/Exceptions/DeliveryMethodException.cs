using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodException(int id):NotFoundException($"The Delivery Method with id {id} Not found")
    {
    }
}
