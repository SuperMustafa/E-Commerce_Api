using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.Order;
using Shared.Dtos.Sequrity;

namespace Services.Abstraction.Authentecation
{
    public interface IAuthenticationService
    {
        public Task<ReturnedUserInfoDto> LoginAsync(UserLoginDto loginDto);
        public Task<ReturnedUserInfoDto> RegiesterAsync(UserRegisterDto regiesterDto);
        public Task<ReturnedUserInfoDto> GetUserByEmail(string email);
        public Task<bool> CheckIfEmailExist(string email);
        public Task<ShipingAddressDto> UpdateUserAddress(ShipingAddressDto addressDto,string email);
        public Task<ShipingAddressDto> GetUserAddress(string email);


    }
}
