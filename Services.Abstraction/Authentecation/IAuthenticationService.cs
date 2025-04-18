using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Sequrity;

namespace Services.Abstraction.Authentecation
{
    public interface IAuthenticationService
    {
        public Task<ReturnedUserInfoDto> LoginAsync(UserLoginDto loginDto);
        public Task<ReturnedUserInfoDto> RegiesterAsync(UserRegisterDto regiesterDto);

    }
}
