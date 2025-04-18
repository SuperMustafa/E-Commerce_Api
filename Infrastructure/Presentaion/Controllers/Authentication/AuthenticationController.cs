using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
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
    }
}
