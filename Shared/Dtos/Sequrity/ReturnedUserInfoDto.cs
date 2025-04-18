using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.Sequrity
{
    public record ReturnedUserInfoDto(string displayName,string email,string tokken)
    {
    }
}
