using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class UserNotFoundexception(string email):NotFoundException($"User With email {email} is not found")
    {
    }
}
