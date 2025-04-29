using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Dtos.Order;
using Shared.Dtos.Sequrity;

namespace Presentaion.Controllers.Authentication
{
    public class AuthenticationController(IServiceManager serviceManager):ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<ReturnedUserInfoDto>> Login(UserLoginDto loginDto)
        {
            var result= await serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ReturnedUserInfoDto>> Register(UserRegisterDto registerDto)
        {
            var result= await serviceManager.AuthenticationService.RegiesterAsync(registerDto);
            return Ok(result);
        }

        [HttpGet("EmailExist")]
        public async Task<ActionResult<bool>> CheckEmailExist(string email)
        {
            return Ok(await serviceManager.AuthenticationService.CheckIfEmailExist(email));
        }

        [Authorize]
        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<ReturnedUserInfoDto>> GetUserByEmail(string email)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email); 
            var result = await serviceManager.AuthenticationService.GetUserByEmail(Email);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<ShipingAddressDto>> GetAddress(string email)
        {
            var user = await serviceManager.AuthenticationService.GetUserAddress(email);
            return Ok(user);
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<ShipingAddressDto>> UpdateAddress(ShipingAddressDto AddressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.AuthenticationService.UpdateUserAddress(AddressDto, email);
            return Ok(result);
        }
    }
}
