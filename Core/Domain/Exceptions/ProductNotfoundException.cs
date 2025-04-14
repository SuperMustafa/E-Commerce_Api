using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ProductNotfoundException:NotFoundException
    {
     public ProductNotfoundException(int id) : base($"Product with id {id} Not Found") { }
    }
}
